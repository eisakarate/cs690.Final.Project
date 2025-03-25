using Xunit;
using Xunit.Abstractions;

using TaskTracker.Models;
using TaskTracker.Processors;

namespace TaskTracker.Test;

public class LoginProcessorTest
{    
    private readonly ITestOutputHelper _output;
    public LoginProcessorTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestLoginNewUser()
    {
        var testUserName = "TestUserNewUserLogin";
        //remove existing file
        if (System.IO.File.Exists(testUserName.GetUserModelFileNameFromUserName()))
            System.IO.File.Delete(testUserName.GetUserModelFileNameFromUserName());
        
        //login
        LoginProcessor.Login(testUserName);
        if (!System.IO.File.Exists(testUserName.GetUserModelFileNameFromUserName()))
            Assert.Fail("User file not created");
        
        if(LoginProcessor.CurrentUser == null)
            Assert.Fail("User Model not created");
    }

    [Fact]
    public void TestLogoutUser()
    {
        var testUserName = "TestUserNewUserLogin";
        //remove existing file
        if (System.IO.File.Exists(testUserName.GetUserModelFileNameFromUserName()))
            System.IO.File.Delete(testUserName.GetUserModelFileNameFromUserName());
        
        //login
        LoginProcessor.Login(testUserName);
        if (!System.IO.File.Exists(testUserName.GetUserModelFileNameFromUserName()))
            Assert.Fail("User file not created");
        
        if(LoginProcessor.CurrentUser == null)
            Assert.Fail("User Model not created");

        //log out
        LoginProcessor.LogOut();
        
        if(LoginProcessor.CurrentUser != null)
            Assert.Fail("User Model not cleared");
    }
    [Fact]
    public void TestLoginExistingUser()
    {
        var testUserName = "TestUserNewUserLogin";
        //remove existing file
        if (System.IO.File.Exists(testUserName.GetUserModelFileNameFromUserName()))
            System.IO.File.Delete(testUserName.GetUserModelFileNameFromUserName());

        //login
        LoginProcessor.LogOut();
        LoginProcessor.Login(testUserName);
        if (!System.IO.File.Exists(testUserName.GetUserModelFileNameFromUserName()))
            Assert.Fail("User file not created");
        
        if(LoginProcessor.CurrentUser == null)
            Assert.Fail("User Model not created");

        //log out
        LoginProcessor.LogOut();

        //log back in 
        LoginProcessor.Login(testUserName);

        if (LoginProcessor.CurrentUser == null)
            Assert.Fail("User Model not created");
    }
}