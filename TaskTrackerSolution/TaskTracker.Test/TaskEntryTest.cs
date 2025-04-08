
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

        Assert.False(tEntry.IsCompleted);
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

        Assert.True(tEntry.IsCompleted);
    }

    public static IEnumerable<object[]> enumValues()
    {
        foreach (var number in Enum.GetValues(typeof(TaskEntryPriority)))
        {
            yield return new object[] { number };
        }
    }

    [Theory]
    [MemberData(nameof(enumValues))]
    public void TestEnums(TaskEntryPriority val)
    {
        Assert.True((Convert.ToInt32(val) >= 0));
    }
}