namespace TaskTracker.Models;

public class TaskEntry
{
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
