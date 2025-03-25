using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

internal partial class userActions
{
    internal static void AddTask()
    {
        var title = AnsiConsole.Prompt(
            new TextPrompt<string>("[green3]Enter a Task Title:[/]")
        );
        var dueDateString = AnsiConsole.Prompt(
            new TextPrompt<string>("[green3]Enter a Due Date (mm/dd/yyyy):[/]")
        );
        var dueTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>("[green3]Enter a Due Time (hh:mm):[/]")
        );
        var priorityString = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ("[green3]Please select a priority:[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
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
            new TextPrompt<string>("[green3][[Optional]]Enter a Description (ok to leave blank):[/]")
            .AllowEmpty()
        );
        var projectName = AnsiConsole.Prompt(
            new TextPrompt<string>("[green3][[Optional]]Enter a Project Name (ok to leave blank):[/]")
            .AllowEmpty()
        );
        var material = AnsiConsole.Prompt(
            new TextPrompt<string>("[green3][[Optional]]Enter a Material List (ok to leave blank):[/]")
            .AllowEmpty()
        );

        var dueDate = DateTime.Parse($"{dueDateString} {dueTimeString}");

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
    }
}