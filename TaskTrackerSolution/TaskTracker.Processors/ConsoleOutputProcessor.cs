using System.IO;
using System.Text.Json;
using TaskTracker.Models;

using Spectre.Console;

namespace TaskTracker.Processors;

public static class ConsoleOutputProcessor
{
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

        //add rows
        foreach(var t in userModel.TaskEntries)
        {
            table.AddRow(
                t.EntryId.ToString(),
                t.Title,
                t.Status.ToString(),
                t.Prioirty.ToString(),
                t.DueDate.ToString(),
                t.Description,
                t.Project,
                t.Material
            );
        }

        return table;
    }
}
