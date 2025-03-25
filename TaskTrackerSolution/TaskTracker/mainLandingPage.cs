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
                break;
            case "Update Task":
                break;
            case "Delete Task":
                break;
            case "Purage Completed Tasks":
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

    internal static void DisplayMainLandingPage()
    {
        if(LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User Model is null.");

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