using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

class mainLandingPageProcessor
{
    private static void displayUseActions()
    {

    }

    internal static void DisplayMainLandingPage()
    {
        if(LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User Model is null.");

        //show status
        var tableToShow = LoginProcessor.CurrentUser.AsSpecterTable();
        AnsiConsole.Write(tableToShow);

        //show user options
        displayUseActions();
    }
}