using System.IO;
using System.Text.Json;
using TaskTracker.Models;

namespace TaskTracker.Processors;

public static class IOOperationsHelper
{
    public static string GetUserModelFileName(this UserModel userModel)
        => $"{userModel.UserName}.json";

    public static string GetUserModelFileNameFromUserName(this string userName)
        => $"{userName}.json";
}

public class IOOperations
{
    /// <summary>
    /// Save the data as JSON
    /// </summary>
    /// <param name="userModel">user model to save</param>
    public static void SaveUserModel(UserModel userModel)
    {
        //write to json
        var json = JsonSerializer.Serialize(userModel);

        //write to file 
        using var fs = File.CreateText(userModel.GetUserModelFileName());
        //write the JSON
        fs.Write(json);
    }

    /// <summary>
    /// Load user model data based onthe user's name
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static UserModel LoadUserModel(string userName)
    {
        //check for the file
        if(!System.IO.File.Exists(userName.GetUserModelFileNameFromUserName())) 
            throw new FileNotFoundException("User's file not found");

        //read the file
        var srcJSON = File.ReadAllText(userName.GetUserModelFileNameFromUserName());

        //deserialize
        var userMdoel = JsonSerializer.Deserialize<UserModel>(srcJSON);

        if(userMdoel == null)
            throw new InvalidOperationException("Could not load user data.");

        //return
        return userMdoel;
    }
}
