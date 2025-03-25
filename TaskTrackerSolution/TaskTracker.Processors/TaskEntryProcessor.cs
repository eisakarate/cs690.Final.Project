using System.Linq;
using TaskTracker.Models;
namespace TaskTracker.Processors;

public static class TaskEntryProcessor
{
    private static bool isTitleRegistered(this UserModel userModel, string title)
        => userModel.TaskEntries.Any(x=>x.Title == title);

    private static int getCurMaxEntryId(this UserModel userModel)
        => userModel.TaskEntries.Count > 0? userModel.TaskEntries.Max(x=>x.EntryId): 0;

    public static void DeleteTask(this UserModel userModel, int entryIdToDelete)
    {
        if(! userModel.TaskEntries.Any(x=>x.EntryId == entryIdToDelete))
            throw new InvalidOperationException("Entry Id not found");
        
        //delete it
        for(var i = 0; i < userModel.TaskEntries.Count; i++)
            if(userModel.TaskEntries[i].EntryId == entryIdToDelete)
                userModel.TaskEntries.Remove(userModel.TaskEntries[i]);
    }
    public static void AddTask(this UserModel userModel, string title, TaskEntryStatus status, TaskEntryPriority priority, DateTime dueDate, string description, string project, string material)
    {
        //validate
        if(userModel.isTitleRegistered(title: title))
            throw new InvalidOperationException("Title is already registered");

        //get the maximum entry id (add 1 to max)
        var newTaskEntryId = userModel.getCurMaxEntryId() + 1;

        //add to the Tasks
        userModel.TaskEntries.Add(new TaskEntry(
            entryId: newTaskEntryId, 
            title: title, 
            status: status, 
            priority: priority, 
            dueDate: dueDate,
            description: description, 
            project: project, 
            material: material
        ));
    }
}
