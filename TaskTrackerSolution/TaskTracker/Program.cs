namespace TaskTracker;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        TaskTracker.Processors.IOOperations.SaveUserModel(null);
    }
}
