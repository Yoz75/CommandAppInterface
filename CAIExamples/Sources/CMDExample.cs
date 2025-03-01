
using Spectre.Console;
using System.IO;

namespace CAI.Examples;

class CMDExample : IExample
{
    private string CurrentDirectory;
    public void Run()
    {
        CurrentDirectory = Directory.GetCurrentDirectory();
        AppInterface cmdInterface = new(caiName: "CMD example", isCatchExceptions: true);
        cmdInterface.AddCommand<string>(new Command<string>("echo", "echo some phrase", Echo, "\"echo\"[phrase]\"\""));
        cmdInterface.AddCommand(new Command("dir", "show current working directory", ShowCurrentDirectory, "\"dir\""));
        cmdInterface.AddCommand<string>(new Command<string>("cd", "change directory", ChangeDirectory, "\"cd [relative path/full path/..]\""));
        cmdInterface.AddCommand(new Command("list", "list files and directories inside current", ListInsideCurrent, "\"list\""));

        cmdInterface.Start();
    }

    private void Echo(string phrase)
    {                     
        AnsiConsole.WriteLine(phrase);
    }

    private void ShowCurrentDirectory()
    {
        AnsiConsole.WriteLine(CurrentDirectory);
    }

    private void ChangeDirectory(string newDir)
    {
        string fullDir = Path.Combine(CurrentDirectory, newDir);

        if(newDir == ".")
        {
            return;
        }
        if(newDir == "..")
        {
            var parentDir = Directory.GetParent(CurrentDirectory);
            CurrentDirectory = parentDir == null ? CurrentDirectory : parentDir.FullName;
            
        }
        else if(Directory.Exists(fullDir))
        {
            CurrentDirectory = fullDir;
            AnsiConsole.MarkupInterpolated($"[green]entered {fullDir} directory![/]\n");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]this directory does not exists![/]");
        }
    }

    private void ListInsideCurrent()
    {
        foreach(var directory in Directory.GetDirectories(CurrentDirectory))
        {
            AnsiConsole.MarkupLineInterpolated($"[gray]{Path.GetRelativePath(CurrentDirectory, directory)}[/]");
        }
        foreach(var file in Directory.GetFiles(CurrentDirectory))
        {
            AnsiConsole.WriteLine(Path.GetFileName(file));
        }
    }
}
