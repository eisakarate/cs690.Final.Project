
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
    
    [Fact]
    public void TestAddTwoNewToEmptyUserModel()
    {
        var userModelToSave = new UserModel(){ UserName = "test"};
        //add one
        userModelToSave.AddTask(
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");
        //add 2
        userModelToSave.AddTask(
        title: "testing2", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test2", 
        project: "Unit Test2", 
        material: "Cheese2");

        //check if item is added
        if(userModelToSave.TaskEntries.Count == 0)
            Assert.Fail("No items added");
        //check if item count is 1
        if(userModelToSave.TaskEntries.Count != 2)
            Assert.Fail($"Expected 2 items got: {userModelToSave.TaskEntries.Count}");
        //check if the entry's EntryId is 1
        if(userModelToSave.TaskEntries[0].EntryId != 1)
            Assert.Fail("Entry Id is not 1");
        //Check if the second entry id is 2
        if(userModelToSave.TaskEntries[1].EntryId != 2)
            Assert.Fail("Entry Id is not 2");
    }    

    [Fact]
    public void TestDuplicateTitleCheck()
    {
        var userModelToSave = new UserModel(){ UserName = "test"};
        try
        {
            //add one
            userModelToSave.AddTask(
            title: "testing", 
            status: TaskEntryStatus.New, 
            priority: TaskEntryPriority.Low, 
            dueDate: DateTime.Now,
            description: "Test", 
            project: "Unit Test", 
            material: "Cheese");
            //add 2
            userModelToSave.AddTask(
            title: "testing", 
            status: TaskEntryStatus.New, 
            priority: TaskEntryPriority.Low, 
            dueDate: DateTime.Now,
            description: "Test2", 
            project: "Unit Test2", 
            material: "Cheese2");

            //Error
            Assert.Fail("Should not be able to add task with the same title");
        }
        catch(InvalidOperationException ex)
        {
            Assert.Equal("Title is already registered", ex.Message);
        }
    }

    [Fact]
    public void TestDeleteTask()
    {
        var userModelToSave = new UserModel(){ UserName = "test"};
        //add one
        userModelToSave.AddTask(
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");
        //add 2
        userModelToSave.AddTask(
        title: "testing2", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test2", 
        project: "Unit Test2", 
        material: "Cheese2");

        //delete the entry
        userModelToSave.DeleteTask(entryIdToDelete: 2);

        //check
        if(userModelToSave.TaskEntries.Count == 2)
            Assert.Fail("No items deleted");
        if(userModelToSave.TaskEntries.Count == 0)
            Assert.Fail("All items deleted");

        //test for failure
        try{
            //delete the entry
            userModelToSave.DeleteTask(entryIdToDelete: 2);

            Assert.Fail("Entry has already been deleted.  Can't delete item that's not there.");
        }
        catch(InvalidOperationException ex)
        {
            Assert.Equal("Entry Id not found", ex.Message);
        }
    }
}
