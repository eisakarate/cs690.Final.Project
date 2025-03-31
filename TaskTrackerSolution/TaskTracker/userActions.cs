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

    internal static void PressEnterToProceed()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Press Enter to proceed:[/]")
            .AllowEmpty()
        );
    }

    internal static void AddTask()
    {
        var title = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Task Title:[/]")
        );
        var dueDateString = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Date (mm/dd/yyyy):[/]")
        );
        var dueTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Time (hh:mm):[/]")
        );
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

        DateTime dueDate;
        if(!DateTime.TryParse($"{dueDateString} {dueTimeString}", out dueDate))
        {
            AnsiConsole.MarkupLine("[red]Invalid date expression provided.[/]");
            AnsiConsole.MarkupLine("[red]Terminating Add process.[/]");
            PressEnterToProceed();
            //display main landing page
            mainLandingPageProcessor.DisplayMainLandingPage();
            return;
        }

        //add to the user model
        if(LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");
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

        AnsiConsole.WriteLine("Done");
        PressEnterToProceed();
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
        Program.loginUser();
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