using Spectre.Console;
using TaskTracker.Models;
using TaskTracker.Processors;

namespace TaskTracker;

internal partial class userActions
{
    private static TaskEntry startUpdateTask()
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
        return LoginProcessor.CurrentUser.TaskEntries.Where(x=>x.EntryId == entryId).First();
    }

    private static void flagAsInProgress(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Status: {trgEntry.StatusForCLI}");
        trgEntry.Status = TaskEntryStatus.InProgress;
        AnsiConsole.WriteLine($"Updated Status: {trgEntry.StatusForCLI}");
        PressEnterToProceed();
    }
    private static void flagAsDone(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Status: {trgEntry.StatusForCLI}");
        trgEntry.Status = TaskEntryStatus.Done;
        AnsiConsole.WriteLine($"Updated Status: {trgEntry.StatusForCLI}");
        PressEnterToProceed();
    }
    private static void updatePriority(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Priority: {trgEntry.PriorityForCLI}");

        //prompt
        var selectedPriority  = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title ($"[{ConfigurationSettings.ConsoleTextColors.Green}]Please select a priority:[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new []{
                        "High",
                        "Mid",
                        "Low"
                    })
                );
        switch(selectedPriority)
        {
            case "High":
                trgEntry.Prioirty = TaskEntryPriority.High;
                break;
            case "Mid":
                trgEntry.Prioirty = TaskEntryPriority.Mid;
                break;
            case "Low":
                trgEntry.Prioirty = TaskEntryPriority.Low;
                break;
        }
        AnsiConsole.WriteLine($"Updated Status: {trgEntry.PriorityForCLI}");
        PressEnterToProceed();
    }
    private static void updateDueDate(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Due Date: {trgEntry.DueDateAsString}");
        
        var dueDateString = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Date (mm/dd/yyyy):[/]")
        );
        var dueTimeString = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Due Time (hh:mm):[/]")
        );    

        DateTime d;
        if(!DateTime.TryParse($"{dueDateString} {dueTimeString}", out d))
        {
            AnsiConsole.WriteLine("[red]Invalid date expression provided.[/]");
            PressEnterToProceed();
            return;
        }

        //set the date
        trgEntry.DueDate = d;

        //display current status
        AnsiConsole.WriteLine($"Updated Due Date: {trgEntry.DueDateAsString}");
        PressEnterToProceed();
    }
    private static void updateDescription(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Description: {trgEntry.DueDateAsString}");
        trgEntry.Description = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Description (ok to leave blank):[/]")
            .AllowEmpty()
        );
        //update
        AnsiConsole.WriteLine($"Updated Description: {trgEntry.DueDateAsString}");
        PressEnterToProceed();
    }
    private static void updateProject(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Project: {trgEntry.Project}");
        trgEntry.Project = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Project Name (ok to leave blank):[/]")
            .AllowEmpty()
        );
        //update
        AnsiConsole.WriteLine($"Updated Project: {trgEntry.Project}");
        PressEnterToProceed();
    }
        
    private static void updateMaterialList(TaskEntry trgEntry)
    {
        //display current status
        AnsiConsole.WriteLine($"Current Material List: {trgEntry.Material}");
        var material = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ConfigurationSettings.ConsoleTextColors.Green}]Enter a Material List (ok to leave blank):[/]")
            .AllowEmpty()
        );
        //update
        AnsiConsole.WriteLine($"Updated Material List: {trgEntry.Material}");
    }

    internal static void displayOptionsAndData(TaskEntry trgEntry)
    {   
        //provide options
        var userSelectedAction = "";

        while(userSelectedAction != "Done")
        {            
            //display the full detail
            AnsiConsole.Write(trgEntry.AsSpecterTable());

            //prompt
            userSelectedAction = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title ("What would you like to do?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new []{
                        "Flag as In-Progress",
                        "Flag as Done",
                        "Update Due Date",
                        "Update Priority",
                        "Update Description",
                        "Update Project",
                        "Update Material List",
                        "Done"
                    })
                );

            //actions
            switch(userSelectedAction)
            {
                case "Flag as In-Progress":
                    flagAsInProgress(trgEntry: trgEntry);
                    break;
                case "Flag as Done":
                    flagAsDone(trgEntry: trgEntry);
                    break;
                case "Update Due Date":
                    updateDueDate(trgEntry: trgEntry);
                    break;
                case "Update Priority":
                    updatePriority(trgEntry: trgEntry);
                    break;
                case "Update Description":
                    updateDescription(trgEntry: trgEntry);
                    break;
                case "Update Project":
                    updateProject(trgEntry: trgEntry);
                    break;
                case "Update Material List":
                    updateMaterialList(trgEntry: trgEntry);
                    break;
                default:
                    break;
            }

            //save the data
            if (LoginProcessor.CurrentUser == null)
                throw new NullReferenceException("User model is null.");
            IOOperations.SaveUserModel(userModel: LoginProcessor.CurrentUser);
        }
    }

    internal static void UpdateTask()
    {
        if (LoginProcessor.CurrentUser == null)
            throw new NullReferenceException("User model is null.");

        //start up the task and display data
        displayOptionsAndData(trgEntry: startUpdateTask());

        //return
        mainLandingPageProcessor.DisplayMainLandingPage();
    }
}