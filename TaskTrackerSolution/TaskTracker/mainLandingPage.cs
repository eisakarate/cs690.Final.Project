using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

class mainLandingPageProcessor
{
    private static void DisplayYouCantDoThisMessage(string message)
    {
        AnsiConsole.MarkupLine($"Ooops! {message}");
        userActions.PressEnterToProceed();
    }

    private static bool CanPerformTaskEntryModificationActions()
    {
        if(LoginProcessor.CurrentUser == null)
            throw new InvalidOperationException("You need to login first");
        return LoginProcessor.CurrentUser.TaskEntries.Count() > 0;
    }
    private static void displayUseActions()
    {
        var userSelectedAction = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ("What would you like to do?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new []{
                    "Add Task",
                    "Delete Task",
                    "Purge Completed Tasks",
                    "Sort Tasks View",
                    "Update Task",
                    "View Task Detail",
                    "Log out",
                    "Quit"
                })
        );

        switch(userSelectedAction)
        {
            case "Sort Tasks View":
                if (CanPerformTaskEntryModificationActions())
                    userActions.SortTaskView();
                else
                {
                    DisplayYouCantDoThisMessage("You haven't registered any tasks.  Please start with the 'Add Task' action.");
                    DisplayMainLandingPage();
                }
                break;
            case "Add Task":
                userActions.AddTask();
                break;
            case "View Task Detail":
                    
                if (CanPerformTaskEntryModificationActions())
                    userActions.ViewTaskDetail();
                else
                {
                    DisplayYouCantDoThisMessage("You haven't registered any tasks.  Please start with the 'Add Task' action.");
                    DisplayMainLandingPage();
                }
                break;
            case "Update Task":                    
                if (CanPerformTaskEntryModificationActions())
                    userActions.UpdateTask();
                else
                {
                    DisplayYouCantDoThisMessage("You haven't registered any tasks.  Please start with the 'Add Task' action.");
                    DisplayMainLandingPage();
                }
                break;
            case "Delete Task":                    
                if (CanPerformTaskEntryModificationActions())
                    userActions.DeleteTask();
                else
                {
                    DisplayYouCantDoThisMessage("You haven't registered any tasks.  Please start with the 'Add Task' action.");
                    DisplayMainLandingPage();
                }
                break;
            case "Purge Completed Tasks":
                userActions.PurgeTasks();
                break;
            case "Log out":
                LoginProcessor.LogOut();
                //clear the screen
                AnsiConsole.Clear();
                //go to the login page
                Program.showWelcomePage();
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
        var rule = new Rule("Trask Tracker Main Page: Task Listing");
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
            AnsiConsole.MarkupLine("No tasks registered yet.  Choose an option from below to get started :backhand_index_pointing_down:");
            AnsiConsole.WriteLine("");
        }
        //show user options
        displayUseActions();
    }
}