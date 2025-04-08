using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

internal partial class userActions
{
    internal static void SortTaskView()
    {
        var userStortAction = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ("What would you like to do?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new []{
                    "By Due Date (Ascending)",
                    "By Due Date (Descending)",
                    "By Title (Ascending)",
                    "By Title (Descending)"
                })
            );

        switch(userStortAction)
        {
            case "By Due Date (Descending)":
                LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByDueDateDescending;
                break;
            case "By Title (Ascending)":
                LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByTitleAscending;
                break;
            case "By Title (Descending)":
                LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByTitleDescending;
                break;
            default:
                LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByDuedDateAscening;
                break;
        }
        mainLandingPageProcessor.DisplayMainLandingPage();
    }

    internal static void PressEnterToProceed(string messageToDsiplay = "Press Enter to return to the Main page")
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]{messageToDsiplay}:[/]")
            .AllowEmpty()
        );
    }

    internal static void AddTask()
    {
        var title = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Task Title:[/]")
        );
        
        var dueDateMonth = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Date (Month):[/]")
                .Validate((n)=>n switch{
                    < 1 => ValidationResult.Error("Must between 1 and 12"),
                    > 12 => ValidationResult.Error("Must between 1 and 12"),
                    >= 1 and <= 12 => ValidationResult.Success()
                })
        );
        var dueDateDay = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Date (Day):[/]")
                .Validate((n)=>n switch{
                    < 1 => ValidationResult.Error("Must between 1 and 31"),
                    > 31 => ValidationResult.Error("Must between 1 and 31"),
                    >= 1 and <= 31 => ValidationResult.Success()
                })
        );

        var dueDateYear = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Date (Year):[/]")
        );

        var dueTimeHour = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Time (Hour: 0 -> mid night, 23 -> 11 pm):[/]")
                .Validate((n)=>n switch{
                    < 0 => ValidationResult.Error("Must between 0 and 23"),
                    > 23 => ValidationResult.Error("Must between 0 and 23"),
                    >= 0 and <= 23 => ValidationResult.Success()
                })
        );
        var dueTimeeMinutes = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Time (Minutes):[/]")
                .Validate((n)=>n switch{
                    < 0 => ValidationResult.Error("Must between 0 and 59"),
                    > 59 => ValidationResult.Error("Must between 0 and 59"),
                    >= 0 and <= 59 => ValidationResult.Success()
                })
        );
        
        //validate date/time expression
        DateTime dueDate;
        if(!DateTime.TryParse($"{dueDateMonth.ToString()}/{dueDateDay.ToString()}/{dueDateYear.ToString()} {dueTimeHour}:{dueTimeeMinutes}", out dueDate))
        {
            AnsiConsole.MarkupLine("[red]Invalid Due Date (Date or Time) expression provided.[/]");
            AnsiConsole.MarkupLine("[red]Terminating Add process.[/]");
            PressEnterToProceed();
            //display main landing page
            mainLandingPageProcessor.DisplayMainLandingPage();
            return;
        }

        var priorityString = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ($"[{ConfigurationSettings.ConsoleTextColors.Green}]Please select a priority:[/]")
                .PageSize(10)
                .MoreChoicesText($"[{ConfigurationSettings.ConsoleTextColors.Grey}](Move up and down to reveal more options)[/]")
                .AddChoices(new []{
                    "High",
                    "Mid",
                    "Low"
                })
        );
        var priority = Models.TaskEntryPriority.Low;
        switch(priorityString)
        {
            case "High":
                priority = Models.TaskEntryPriority.High;
                break;
            case "Mid":
                priority = Models.TaskEntryPriority.Mid;
                break;
            case "Low":
                priority = Models.TaskEntryPriority.Low;
                break;
        }
        var description = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}][[Optional]]Enter a Description (ok to leave blank):[/]")
            .AllowEmpty()
        );
        var projectName = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}][[Optional]]Enter a Project Name (ok to leave blank):[/]")
            .AllowEmpty()
        );
        var material = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}][[Optional]]Enter a Material List (ok to leave blank):[/]")
            .AllowEmpty()
        );

        //add to the user model
        if(LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");
        try
        {
            LoginProcessor.CurrentUser.AddTask(
                title: title,
                status: Models.TaskEntryStatus.New,
                priority: priority,
                dueDate: dueDate,
                description: description,
                project: projectName,
                material: material
            );
            //save the data
            IOOperations.SaveUserModel(userModel: LoginProcessor.CurrentUser);

            AnsiConsole.WriteLine("Sucessfully added new task.");
        }
        catch(InvalidOperationException ex)
        {
        AnsiConsole.MarkupLine($"[red1 bold]Error encountered while adding the task: {ex.Message}![/]");
        AnsiConsole.MarkupLine($"[red1 bold]Add process has been terminated.[/]");
        }
        PressEnterToProceed("Press Enter to return to the Main page");
        //display main landing page
        mainLandingPageProcessor.DisplayMainLandingPage();
    }
    internal static void LogoutAction()
    {
        //clear the screen
        AnsiConsole.Clear();

        //log out
        LoginProcessor.LogOut();

        //login
        Program.showWelcomePage();
    }

    internal static void QuitAction()
    {
        //clear the screen
        AnsiConsole.Clear();

        //log out
        LoginProcessor.LogOut();

        //terminate
        AnsiConsole.WriteLine("Good bye");

        Environment.Exit(0);
    }
}