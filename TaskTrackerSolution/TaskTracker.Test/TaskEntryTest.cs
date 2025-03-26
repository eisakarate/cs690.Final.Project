
using TaskTracker.Models;

namespace TaskTracker.Test;

public class TaskEntryTest
{
    [Fact]
    public void TestTaskEntryIsDone_notDone()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        Assert.Equal(false, tEntry.IsCompleted);
    }
    [Fact]
    public void TestTaskEntryIsDone_isDone()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.Done, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        Assert.Equal(true, tEntry.IsCompleted);
    }
}