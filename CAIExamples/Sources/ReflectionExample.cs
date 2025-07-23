using CAI.Reflection;
using Spectre.Console;
using System.IO;

namespace CAI.Examples;

public class ReflectionExample : IExample
{
    public void Run()
    {
        var appInterface = new AppInterface("reflection", true);
        appInterface.LoadAttributedCommands();
        //You can mix reflection and regular commands
        appInterface.AddCommand(new Command("cmd", "load reflection cmd", () =>
        {
            ReflectionCMD reflectionCMD = new();
            reflectionCMD.Start();
        }, "cmd"));
        appInterface.Start();
    }

    [Command("reflection", "echo", "Echoes the input text", "echo [text]")]
    public static void Echo(string value)
    {
        AnsiConsole.WriteLine($"{value}");
    }

    [Command("reflection", "bar", "say bar", "bar")]
    public static void Bar()
    {
        AnsiConsole.WriteLine("bar");
    }
}

//Actually i could do this without singletone, just static variables, but i wanted to show 
//how to make an app interface when you have to store nonstatic variables
internal class ReflectionCMD
{
    private static ReflectionCMD Instance;
    private string CurrentDirectory;

    public void Start()
    {
        Instance = this;

        AnsiConsole.Clear();

        CurrentDirectory = Directory.GetCurrentDirectory();

        AppInterface appInterface = new AppInterface("reflection cmd", true);
        appInterface.LoadAttributedCommands();
        appInterface.Start();
    }

    [Command("reflection cmd", "echo", "echo some phrase", "echo [phrase]")]
    public static void Echo(string phrase)
    {
        AnsiConsole.WriteLine(phrase);
    }

    [Command("reflection cmd", "dir", "show current directory", "dir")]

    public static void ShowCurrentDirectory()
    {
        AnsiConsole.WriteLine(Instance.CurrentDirectory);
    }

    [Command("reflection cmd", "cd", "change directory", "cd [relative path/full path/..]")]
    public static void ChangeDirectory(string newDir)
    {
        string fullDir = Path.Combine(Instance.CurrentDirectory, newDir);

        if(newDir == ".")
        {
            return;
        }
        if(newDir == "..")
        {
            var parentDir = Directory.GetParent(Instance.CurrentDirectory);
            Instance.CurrentDirectory = parentDir == null ? Instance.CurrentDirectory : parentDir.FullName;

        }
        else if(Directory.Exists(fullDir))
        {
            Instance.CurrentDirectory = fullDir;
            AnsiConsole.MarkupInterpolated($"[green]entered {fullDir} directory![/]\n");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]this directory does not exists![/]");
        }
    }

    [Command("reflection cmd", "list", "list files and directories inside current", "list")]
    public static void ListInsideCurrent()
    {
        foreach(var directory in Directory.GetDirectories(Instance.CurrentDirectory))
        {
            AnsiConsole.MarkupLineInterpolated($"[gray]{Path.GetRelativePath(Instance.CurrentDirectory, directory)}[/]");
        }
        foreach(var file in Directory.GetFiles(Instance.CurrentDirectory))
        {
            AnsiConsole.WriteLine(Path.GetFileName(file));
        }
    }

    [Command("reflection cmd", "mkdir", "make a new directory", "mkdir [name]")]

    public static void MakeDirectory(string name)
    {
        Directory.CreateDirectory(Path.Combine(Instance.CurrentDirectory, name));
        AnsiConsole.MarkupLineInterpolated($"[green]made {name} [/]");
    }
}