using System.Collections.Frozen;
using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

internal partial class userActions
{
    internal static void DeleteTask()
    {
        if (LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");

        var entryId = AnsiConsole.Prompt(
            new TextPrompt<int>("[red]Enter an Entry Id:[/]")
        );

        //find the entry
        if(!LoginProcessor.CurrentUser.TaskEntries.Any(x=>x.EntryId == entryId))
        {
            AnsiConsole.WriteLine("Entry Id Not found");
            mainLandingPageProcessor.DisplayMainLandingPage();
        }

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
        mainLandingPageProcessor.DisplayMainLandingPage();
    }
}