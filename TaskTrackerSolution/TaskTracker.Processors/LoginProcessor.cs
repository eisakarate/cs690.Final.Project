using TaskTracker.Models;

namespace TaskTracker.Processors;

public static class LoginProcessor
{
    //User Model
    public static UserModel? CurrentUser;

    public static void Login(string userName)
    {
        System.Diagnostics.Debug.WriteLine("Logging in");
        //check if they can log in
        // if(CurrentUser != null)
        //     throw new InvalidOperationException("You cannot login until current user logs out.");

        //log in
        CurrentUser = IOOperations.LoadUserModel(userName);
    }

    public static void LogOut()
    {
        System.Diagnostics.Debug.WriteLine("Logging out");

        if(CurrentUser == null)
            return; //nothing to do, just log out

        //save the data
        IOOperations.SaveUserModel(CurrentUser);

        //clear the current user model, add an empty class
        CurrentUser = null;// new UserModel();
    }
}
