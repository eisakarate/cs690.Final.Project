
using TaskTracker.Models;
using TaskTracker.Processors;

namespace TaskTracker.Test;

public class TasKEntryProcessorTest
{
    [Fact]
    public void TestAddNewToEmptyUserModel()
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

        userModelToSave.AddTask(
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        //check if item is added
        if(userModelToSave.TaskEntries.Count == 0)
            Assert.Fail("No items added");
        //check if item count is 1
        if(userModelToSave.TaskEntries.Count != 1)
            Assert.Fail($"More than 1 item added: {userModelToSave.TaskEntries.Count}");
        //check if the entry's EntryId is 1
        if(userModelToSave.TaskEntries[0].EntryId != 1)
            Assert.Fail("Entry Id is not 1");
    }
}