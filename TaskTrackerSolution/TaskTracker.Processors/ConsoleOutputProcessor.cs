using System.IO;
using System.Text.Json;
using TaskTracker.Models;

using Spectre.Console;

namespace TaskTracker.Processors;

public static class ConsoleOutputProcessor
{
    public static Table AsSpecterTable(this TaskEntry task)
    {
        // Create a table
        var table = new Spectre.Console.Table();

        //add colums
        table.AddColumn("Property");
        table.AddColumn("Value");

        //add entries
        table.AddRow("Entry Id",task.EntryId.ToString());
        table.AddRow("Title",task.TitleForCLI);
        table.AddRow("Status",task.StatusForTable);
        table.AddRow("Priority",task.PriorityForTable);
        table.AddRow("Due Date",task.DueDateAsString);
        table.AddRow("Project",task.Project);
        table.AddRow("Material",task.Material);

        return table;
    }

    public static Table AsSpecterTable(this UserModel userModel)
    {
        // Create a table
        var table = new Spectre.Console.Table();

        //add colums
        table.AddColumn("Entry Id");
        table.AddColumn("Title");
        table.AddColumn("Status");
        table.AddColumn("Priority");
        table.AddColumn("Due Date");
        table.AddColumn("Description");
        table.AddColumn("Project");
        table.AddColumn("Material");

        //sort options
        List<TaskEntry> sorted = null;
        switch(LoginProcessor.TaskSortOptions)
        {
            case TaskTableSortOptions.ByDueDateDescending:
                sorted = userModel.TaskEntries.OrderByDescending(x=>x.DueDate).ToList();
                break;
            case TaskTableSortOptions.ByTitleAscending:
                sorted = userModel.TaskEntries.OrderBy(x=>x.Title).ToList();
                break;
            case TaskTableSortOptions.ByTitleDescending:
                sorted = userModel.TaskEntries.OrderByDescending(x=>x.Title).ToList();
                break;
            default:
                sorted = userModel.TaskEntries.OrderBy(x=>x.DueDate).ToList();
                break;
        }

        //add rows
        foreach(var t in sorted)
        {
            table.AddRow(
                t.EntryId.ToString(),
                t.TitleForCLI,
                t.StatusForTable,
                t.PriorityForTable,
                t.DueDateAsString,
                t.Description,
                t.Project,
                t.Material
            );
        }

        return table;
    }
}
