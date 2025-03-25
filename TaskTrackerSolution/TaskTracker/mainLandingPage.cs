using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

class mainLandingPageProcessor
{
    private static void displayUseActions()
    {
        var userSelectedAction = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ("What would you like to do?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new []{
                    "Add Task",
                    "View Task Detail",
                    "Update Task",
                    "Delete Task",
                    "Purge Completed Tasks",
                    "Log out",
                    "Quit"
                })
        );

        switch(userSelectedAction)
        {
            case "Add Task":
                userActions.AddTask();
                break;
            case "View Task Detail":
                userActions.ViewTaskDetail();
                break;
            case "Update Task":
                userActions.UpdateTask();
                break;
            case "Delete Task":
                userActions.DeleteTask();
                break;
            case "Purage Completed Tasks":
                userActions.PurgeTasks();
                break;
            case "Log out":
                userActions.LogoutAction();
                break;
            case "Quit":
                userActions.QuitAction();
                break;
            default:
                break;
        }
    }

    internal static void DisplayMainLandingPage(string messageToDisplayOnLoad = "")
    {
        if(LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User Model is null.");

        AnsiConsole.Clear();
        AnsiConsole.WriteLine("Main Page");
        AnsiConsole.WriteLine($"User: {LoginProcessor.CurrentUser.UserName}");

        if(messageToDisplayOnLoad.Length > 0)
            AnsiConsole.Write($"[{ConfigurationSettings.ConsoleTextColors.Gold}]{messageToDisplayOnLoad}[]/");

        //show status
        var tableToShow = LoginProcessor.CurrentUser.AsSpecterTable();

        if(tableToShow.Rows.Count > 0)
            AnsiConsole.Write(tableToShow);
        else
            AnsiConsole.WriteLine("No tasks here.");

        //show user options
        displayUseActions();
    }
}