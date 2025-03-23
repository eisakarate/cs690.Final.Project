using System.Collections.Generic;

namespace TaskTracker.Models;

public class UserModel
{
    public string UserName {get; set;}

    public List<TaskEntry> TaskEntries{get; private set;} = new List<TaskEntry>();
}
