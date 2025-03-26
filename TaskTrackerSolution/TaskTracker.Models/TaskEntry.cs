namespace TaskTracker.Models;

public class TaskEntry
{
    public TaskEntry() { }

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
        Material = material;
    }

    public int EntryId{get; set;}
    public string Title {get; set;} = "";
    public string TitleForCLI{
        get{
            return Title;
        }
    }

    public TaskEntryStatus Status{get; set;}
    public string StatusForTable{
        get{
            switch(Status)
            {
                case TaskEntryStatus.New:
                    return $"[white]New[/]";
                case TaskEntryStatus.InProgress:
                    return $"[gold1]In-Progress[/]";
                case TaskEntryStatus.Done:
                    return $"[green3]Done[/]";
            }
            return "";
        }
    }
    public string StatusForCLI{
        get{
            switch(Status)
            {
                case TaskEntryStatus.New:
                    return "New";
                case TaskEntryStatus.InProgress:
                    return "In-Progress";
                case TaskEntryStatus.Done:
                    return "Done";
            }
            return "";
        }
    }

    public TaskEntryPriority Prioirty {get; set;}
    public string PriorityForTable{
        get{
            switch(Prioirty)
            {
                case TaskEntryPriority.High:
                    return $"[red1]High[/]";
                case TaskEntryPriority.Mid:
                    return $"[Yellow]Mid[/]";
                case TaskEntryPriority.Low:
                    return $"[Grey]Low[/]";
            }
            return "";
        }
    }
    
    public string PriorityForCLI{
        get{
            switch(Prioirty)
            {
                case TaskEntryPriority.High:
                    return "High";
                case TaskEntryPriority.Mid:
                    return "Mid";
                case TaskEntryPriority.Low:
                    return "Low";
            }
            return "";
        }
    }

    public DateTime DueDate {get; set;}

    public string DueDateAsString => $"{DueDate.ToShortDateString()} {DueDate.ToShortTimeString()}";

    public string Description {get; set;} = "";

    public string Project {get; set;} = "";

    public string Material {get; set;} = "";

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
