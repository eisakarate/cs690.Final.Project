using Spectre.Console;
using Spectre.Console.Cli;

using TaskTracker.Processors;

namespace TaskTracker;

class Program
{
    private static string promptForLogin()
    {
        AnsiConsole.WriteLine("This is your first time using the Task Tracker? Awesome!. This application will automatically create a new account for you when you login.");
        
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter your User Name:")
        );
    }

    public static void loginUser()
    {
        var userName = promptForLogin();

        //log the user in
        LoginProcessor.Login(userName: userName);

        //got to the main page
        mainLandingPageProcessor.DisplayMainLandingPage();
    }

    public static void showWelcomePage()
    {   
        var rule = new Rule();
        AnsiConsole.Write(rule);
        AnsiConsole.MarkupLine("[gold1 bold]Welcome to Task Tracker![/]");
        AnsiConsole.Write(rule);
        
        var userAction = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title ("What would you like to do?")
                .PageSize(10)
                .AddChoices(new []{
                    "Login to the application",
                    "Quit the application",
                })
            );
        
        switch(userAction)
        {
            case "Login to the application":
                loginUser();
                break;
            default:
                userActions.QuitAction();
                break;
        }
    }

    static void Main(string[] args)
    {
        showWelcomePage();
    }
}
