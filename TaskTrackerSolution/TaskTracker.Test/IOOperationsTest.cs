
using TaskTracker.Models;
using TaskTracker.Processors;

namespace TaskTracker.Test;

public class IOOperationsTest
{
    [Fact]
    public void TestUserModelSave()
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

        //save it
        IOOperations.SaveUserModel(userModelToSave);

        //check to see if the file exists
        if(!System.IO.File.Exists(userModelToSave.GetUserModelFileName()))
            Assert.Fail(message:"User Model not saved.");
    }
    [Fact]
    public void TestUserModelLoad()
    {
        var tEntry = new TaskEntry(entryId: 1, 
        title: "testing", 
        status: TaskEntryStatus.New, 
        priority: TaskEntryPriority.Low, 
        dueDate: DateTime.Now,
        description: "Test", 
        project: "Unit Test", 
        material: "Cheese");

        var userModelToSave = new UserModel(){ UserName = "test2"};
        userModelToSave.TaskEntries.Add(tEntry);

        //save it
        IOOperations.SaveUserModel(userModelToSave);

        //check to see if the file exists
        if(!System.IO.File.Exists(userModelToSave.GetUserModelFileName()))
            Assert.Fail(message:"User Model not saved.");
        
        //lowd it
        var loadedUserModel = IOOperations.LoadUserModel(userName: userModelToSave.UserName);

        //check if the information is equal
        Assert.Equal(userModelToSave.UserName, loadedUserModel.UserName);
    }
}