
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
}