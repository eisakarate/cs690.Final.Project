using Spectre.Console;
using Spectre.Console.Cli;

using TaskTracker.Processors;

namespace TaskTracker;

class Program
{
    private static string promptForLogin()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter your User Name:")
        );
    }

    private static void loginUser()
    {
        var userName = promptForLogin();

        //log the user in
        LoginProcessor.Login(userName: userName);
    }

    static void Main(string[] args)
    {
        AnsiConsole.WriteLine("Welcome to Task Tracker!");
        
        loginUser();
    }
}
