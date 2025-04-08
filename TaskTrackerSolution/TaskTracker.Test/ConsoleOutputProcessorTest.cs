
using TaskTracker.Models;
using TaskTracker.Processors;

namespace TaskTracker.Test;

public class ConsoleOutputProcessorTest
{
    [Fact]
    public void TestRenderAsTable()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        var userModelToSave = new UserModel(){ UserName = "test"};
        userModelToSave.TaskEntries.Add(tEntry);

        var table = userModelToSave.AsSpecterTable();
    }

    [Fact]
    public void TestTasksForConsoleChoices()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

         var tEntry2 = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.Done, 
        priority: TaskEntryPriority.Mid, 
        dueDate: DateTime.Now.AddMinutes(-14),
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        var tEntry3 = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.InProgress, 
        priority: TaskEntryPriority.High, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        var userModelToSave = new UserModel(){ UserName = "test"};
        userModelToSave.TaskEntries.Add(tEntry);
        userModelToSave.TaskEntries.Add(tEntry2);
        userModelToSave.TaskEntries.Add(tEntry3);

        //get the output
        var ret = userModelToSave.TasksForConsoleChoices();

        //we should have three
        Assert.Equal(3, ret.Count());
    }

    [Fact]
    public void TestTaskAsSpecterTable()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");
        tEntry.AsSpecterTable();
    }

    [Fact]
    public void TestRenderEntryAsTable()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

         var tEntry2 = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.Done, 
        priority: TaskEntryPriority.Mid, 
        dueDate: DateTime.Now.AddMinutes(-14),
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        var tEntry3 = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.InProgress, 
        priority: TaskEntryPriority.High, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        var userModelToSave = new UserModel(){ UserName = "test"};
        userModelToSave.TaskEntries.Add(tEntry);
        userModelToSave.TaskEntries.Add(tEntry2);
        userModelToSave.TaskEntries.Add(tEntry3);
        
        LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByDuedDateAscening;
        foreach (var e in userModelToSave.TaskEntries)
        {
            var table = e.AsSpecterTable();
        }
        LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByDueDateDescending;
        foreach (var e in userModelToSave.TaskEntries)
        {
            var table = e.AsSpecterTable();
        }
        LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByTitleAscending;
        foreach (var e in userModelToSave.TaskEntries)
        {
            var table = e.AsSpecterTable();
        }
        LoginProcessor.TaskSortOptions = TaskTableSortOptions.ByTitleDescending;
        foreach (var e in userModelToSave.TaskEntries)cd
        {
            var table = e.AsSpecterTable();
        }
    }
}