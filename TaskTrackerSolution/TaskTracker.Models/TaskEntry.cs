namespace TaskTracker.Models;

public class TaskEntry
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="entryid"></param>
    /// <param name="title"></param>
    /// <param name="status"></param>
    /// <param name="priority"></param>
    /// <param name="dueDate"></param>
    /// <param name="description"></param>
    /// <param name="project"></param>
    /// <param name="material"></param>
    public TaskEntry(int entryId, string title, TaskEntryStatus status, TaskEntryPriority priority, DateTime dueDate, string description, string project, string material )
    {
        EntryId = entryId;
        Title = title;
        Status = status;
        Prioirty = priority;
        DueDate = dueDate;
        Description = description;
        Project = project;
        material = Material;
    }

    public int EntryId{get; private set;}
    public string Title {get; private set;}

    public TaskEntryStatus Status{get; set;}

    public TaskEntryPriority Prioirty {get; set;}

    public DateTime DueDate {get; set;}

    public string Description {get; set;}

    public string Project {get; set;}

    public string Material {get; set;}

    public bool IsCompleted {
        get{
            switch (Status)
            {
                case TaskTracker.Models.TaskEntryStatus.Done:
                    return true;
                default: 
                    return false;
            }
        }
    }

    public bool IsOverdue{
        get{
            return DueDate > DateTime.Now;
        }
    }
}
