using Spectre.Console;

using TaskTracker.Processors;

namespace TaskTracker;

internal partial class userActions
{
    internal static void UpdateTask()
    {
        if (LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");

        PressEnterToProceed();
        mainLandingPageProcessor.DisplayMainLandingPage();
    }
}