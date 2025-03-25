using System.Collections.Frozen;
using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

internal partial class userActions
{
    internal static void PurgeTasks()
    {
        if (LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");

        mainLandingPageProcessor.DisplayMainLandingPage();
    }
    internal static void DeleteTask()
    {
        if (LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");

        var entryId = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Red}]Enter an Entry Id:[/]")
        );

        //find the entry
        if(!LoginProcessor.CurrentUser.TaskEntries.Any(x=>x.EntryId == entryId))
        {
            AnsiConsole.WriteLine("Entry Id Not found");
            mainLandingPageProcessor.DisplayMainLandingPage();
        }

        //get the item
        var trgEntry = LoginProcessor.CurrentUser.TaskEntries.Where(x=>x.EntryId == entryId).First();

        // Ask the user to confirm
        var confirmation = AnsiConsole.Prompt(
            new ConfirmationPrompt($"$ Are you sure you want to delete Task {entryId}: {trgEntry.Title}?"));

        //yes or No?
        if(!confirmation)
            mainLandingPageProcessor.DisplayMainLandingPage();

        //delete
        LoginProcessor.CurrentUser.DeleteTask(entryIdToDelete: entryId);
        IOOperations.SaveUserModel(LoginProcessor.CurrentUser);

        //notify
        AnsiConsole.WriteLine("Entry removed successfully.");
        PressEnterToProceed();
        mainLandingPageProcessor.DisplayMainLandingPage();
    }

    internal static void ViewTaskDetail()
    {
        if (LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");

        var entryId = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ConfigurationSettings.ConsoleTextColors.Blue}]Enter an Entry Id:[/]")
        );

        //find the entry
        if(!LoginProcessor.CurrentUser.TaskEntries.Any(x=>x.EntryId == entryId))
        {
            AnsiConsole.WriteLine("Entry Id Not found");
            PressEnterToProceed();
            mainLandingPageProcessor.DisplayMainLandingPage();
        }
        
        //get the item
        var trgEntry = LoginProcessor.CurrentUser.TaskEntries.Where(x=>x.EntryId == entryId).First();

        //display the detail
        AnsiConsole.Write(trgEntry.AsSpecterTable());

        //display user options
        var userSelectedAction = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ("What would you like to do?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new []{
                    "Exit, return to main page",
                    "Update Task",
                    "Delete Task"
                })
        );
        switch(userSelectedAction)
        {
            case "Update Task":
                UpdateTask();
                break;
            case "Delete Task":
                DeleteTask();
                break;
            default:
                //exit
                mainLandingPageProcessor.DisplayMainLandingPage();
                break;
        }
    }
}