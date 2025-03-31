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
                    "Sort Tasks View",
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
            case "Sort Tasks View":
                userActions.SortTaskView();
                break;
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
            case "Purge Completed Tasks":
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
        var rule = new Rule("Welcome To the Main Page");
        rule.RuleStyle("gold3 bold");
        AnsiConsole.Write(rule);
        AnsiConsole.MarkupLine($"User: [gold3 bold]{LoginProcessor.CurrentUser.UserName}[/]");
        var rule2= new Rule();
        rule2.RuleStyle("gold3 bold");
        AnsiConsole.Write(rule2);

        if(messageToDisplayOnLoad.Length > 0)
            AnsiConsole.Write($"[{ConfigurationSettings.ConsoleTextColors.Gold}]{messageToDisplayOnLoad}[]/");

        //show status
        var tableToShow = LoginProcessor.CurrentUser.AsSpecterTable();

        if(tableToShow.Rows.Count > 0)
            AnsiConsole.Write(tableToShow);
        else
        {
            AnsiConsole.WriteLine("");
            AnsiConsole.MarkupLine("No tasks defined.  Choose an option from below to get started :backhand_index_pointing_down:");
            AnsiConsole.WriteLine("");
        }
        //show user options
        displayUseActions();
    }
}