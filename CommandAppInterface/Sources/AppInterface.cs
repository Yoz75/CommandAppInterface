
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAI;

/// <summary>
/// App command interface itself.
/// Several commands are already included!!!!!
/// </summary>
public class AppInterface
{
    private List<IDescribable> CommandsAsDescribable = new();
    private Dictionary<string, Action<string>> CommandCallers = new();

    //Yeah, I use build number as patch version😎😎😎
    private readonly Version CAIVersion = new(1, 3, 1);

    private bool IsEnded = false;

    #region String Constants
    private const string SpectreConsoleCopyright =
        "[aqua]CAI uses Spectre.Console: Copyright (c) 2020 Patrik Svensson, Phil Scott, Nils Andresen[/]";
    private const string UnknownCommandErrorMessage = "[red]Unknown command! Type help for more info![/]\n";
    private const string PanicMessage = "[red]Panic! App has thrown an exception:[/]\n";
    private const string PanicPrompt = "[underline red]Panic![/]\n[grey]Something went REALLY wrong! Continue work or exit?[/]";
    private const string WriteNextCommandMessage = "[dodgerblue1]Write next command:[/]";
    #endregion

    private bool IsCatchExceptions;

    private string Name;

    private const InterfaceStyles StandardStyle = InterfaceStyles.Full;

    private InterfaceStyles Style;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="caiName">current CAI name, use if you want to split the interface into several (just for beauty)</param>
    /// /// <param name="isCatchExceptions">if true, CAU will catch exceptions 
    /// and write their messages on console</param>
    public AppInterface(string caiName, bool isCatchExceptions = false, InterfaceStyles style = StandardStyle)
    {
        Name = caiName;
        IsCatchExceptions = isCatchExceptions;
        Style = style;

        AddCommand(new Command("help", "write the All-Commands-Table", WriteCommands, "\"help\""));
        AddCommand(new Command("clear", "clear screen", AnsiConsole.Clear, "\"clear\""));
        AddCommand(new Command("quit", "quit CAI", Quit, "\"quit\""));
        AddCommand(new Command("name", "get current CAI name", () =>
        {
            if(!string.IsNullOrEmpty(Name))
            {
                AnsiConsole.WriteLine(Name);
            }
        }, "\"name\""));
    }

    public void Start()
    {
        string welcomeCAIBanner = $"[lime]Command App Interface ver{CAIVersion.ToString()}[/]";

        if((Style & InterfaceStyles.WelcomeMessage) != 0)
        {

            var welcomeRule = new Rule(welcomeCAIBanner);

            AnsiConsole.Write(welcomeRule);
        }
        if((Style & InterfaceStyles.CopyrightMessage) != 0)
        {
            var spectreText = new Markup(SpectreConsoleCopyright).Justify(Justify.Center);
            AnsiConsole.Write(spectreText);
            AnsiConsole.WriteLine();
        }
        if((Style & InterfaceStyles.CAIName) != 0)
        {
            if(!string.IsNullOrEmpty(Name))
                AnsiConsole.Write(
                    Markup.FromInterpolated(
                        $"[aqua]Started Command App Interface named {Name}[/]\n").Justify(Justify.Center)
                    );
        }

        while(!IsEnded)
        {
            try
            {
                string nextCommandPrompt = "";
                if((Style & InterfaceStyles.WriteCommandPrompt) != 0)
                {
                    nextCommandPrompt = WriteNextCommandMessage;
                }
                string rawCommand = AnsiConsole.Ask<string>(nextCommandPrompt);

                if(string.IsNullOrEmpty(rawCommand) || !CommandCallers.ContainsKey(GetCommandName(rawCommand)))
                {
                    AnsiConsole.Markup(UnknownCommandErrorMessage);
                    continue;
                }

                CommandCallers[GetCommandName(rawCommand)](rawCommand);
            }
            catch(Exception ex) when(IsCatchExceptions)
            {
                AnsiConsole.Write(new Markup(PanicMessage));
                AnsiConsole.MarkupLineInterpolated($"[darkred_1]{ex.Message}[/]");
                AnsiConsole.Markup("[yellow]stack trace:[/]");
                AnsiConsole.MarkupLineInterpolated($"[yellow]{ex.StackTrace}[/]\n");

                const string exitAnswer = "exit";
                const string continueAnswer = "continue";

                var panicPrompt = new SelectionPrompt<string>();
                panicPrompt.Title = PanicPrompt;
                panicPrompt.AddChoices([exitAnswer, continueAnswer]);

                var answer = AnsiConsole.Prompt(panicPrompt);

                switch(answer)
                {
                    case exitAnswer:
                        Quit();
                        return;

                    case continueAnswer:
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Call this method from a command (e.g "quit") to end CAI work.
    /// </summary>
    public void Quit()
    {
        IsEnded = true;
    }

    public void WriteInfo(string message, string prompt = "Info")
    {
        AnsiConsole.MarkupLineInterpolated($"[green]{prompt}:[/]\n{message}");
    }

    public void WriteInfo(Spectre.Console.Rendering.IRenderable renderable)
    {
        AnsiConsole.Write(renderable);
    }

    #region AddCommand
    /// <summary>
    /// Add a new command, you can also rewrite existing and built-in commands 
    /// </summary>
    public void AddCommand(Command command)
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (_) => { command.Action(); };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddCommand<Argument1>(Command<Argument1> command) where Argument1 : IParsable<Argument1>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(ParseArgument<Argument1>(rawCommand, 0));
        };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddVariableArgsCommand<Argument>(VariableArgsCommand<Argument> command) where Argument : IParsable<Argument>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(ParseArguments<Argument>(rawCommand));
        };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddCommand<Argument1, Argument2>(Command<Argument1, Argument2> command)
        where Argument1 : IParsable<Argument1>
        where Argument2 : IParsable<Argument2>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(
                ParseArgument<Argument1>(rawCommand, 0),
                ParseArgument<Argument2>(rawCommand, 1)
                );
        };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddCommand<Argument1, Argument2, Argument3>(
        Command<Argument1, Argument2, Argument3> command)
        where Argument1 : IParsable<Argument1>
        where Argument2 : IParsable<Argument2>
        where Argument3 : IParsable<Argument3>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(
                ParseArgument<Argument1>(rawCommand, 0),
                ParseArgument<Argument2>(rawCommand, 1),
                ParseArgument<Argument3>(rawCommand, 2)
                );
        };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddCommand<Argument1, Argument2, Argument3, Argument4>(
        Command<Argument1, Argument2, Argument3, Argument4> command)
        where Argument1 : IParsable<Argument1>
        where Argument2 : IParsable<Argument2>
        where Argument3 : IParsable<Argument3>
        where Argument4 : IParsable<Argument4>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(
                ParseArgument<Argument1>(rawCommand, 0),
                ParseArgument<Argument2>(rawCommand, 1),
                ParseArgument<Argument3>(rawCommand, 2),
                ParseArgument<Argument4>(rawCommand, 3)
                );
        };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddCommand<Argument1, Argument2, Argument3, Argument4, Argument5>(
        Command<Argument1, Argument2, Argument3, Argument4, Argument5> command)
        where Argument1 : IParsable<Argument1>
        where Argument2 : IParsable<Argument2>
        where Argument3 : IParsable<Argument3>
        where Argument4 : IParsable<Argument4>
        where Argument5 : IParsable<Argument5>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(
                ParseArgument<Argument1>(rawCommand, 0),
                ParseArgument<Argument2>(rawCommand, 1),
                ParseArgument<Argument3>(rawCommand, 2),
                ParseArgument<Argument4>(rawCommand, 3),
                ParseArgument<Argument5>(rawCommand, 4)
                );
        };
    }

    /// <summary>
    /// Add a new command
    /// </summary>
    public void AddCommand<Argument1, Argument2, Argument3, Argument4, Argument5, Argument6>(
        Command<Argument1, Argument2, Argument3, Argument4, Argument5, Argument6> command)
        where Argument1 : IParsable<Argument1>
        where Argument2 : IParsable<Argument2>
        where Argument3 : IParsable<Argument3>
        where Argument4 : IParsable<Argument4>
        where Argument5 : IParsable<Argument5>
        where Argument6 : IParsable<Argument6>
    {
        CommandsAsDescribable.Add(command);
        CommandCallers[command.GetName()] = (rawCommand) =>
        {
            command.Action(
                ParseArgument<Argument1>(rawCommand, 0),
                ParseArgument<Argument2>(rawCommand, 1),
                ParseArgument<Argument3>(rawCommand, 2),
                ParseArgument<Argument4>(rawCommand, 3),
                ParseArgument<Argument5>(rawCommand, 4),
                ParseArgument<Argument6>(rawCommand, 4)
                );
        };
    }
    #endregion

    private void WriteCommands()
    {
        Table allCommandsTable = new Table();
        allCommandsTable.Title("The All-Commands-Table");

        allCommandsTable.AddColumn(new TableColumn("names"));
        allCommandsTable.AddColumn(new TableColumn("descriptions"));
        allCommandsTable.AddColumn(new TableColumn("usage"));

        foreach(var command in CommandsAsDescribable)
        {
            allCommandsTable.AddRow(
                Markup.FromInterpolated($"{command.GetName()}"),
                Markup.FromInterpolated($"{command.GetDescription()}"),
                Markup.FromInterpolated($"{command.GetUsage()}"));
        }
        AnsiConsole.Write(allCommandsTable);
    }

    private string GetCommandName(string rawCommand)
    {
        return rawCommand.Split(' ')[0];
    }
    private T ParseArgument<T>(string rawCommand, int argumentIndex) where T : IParsable<T>
    {
        try
        {
            List<string> substrings = new();

            StringBuilder sb = new();
            bool isAppendingRawString = false;
            foreach(var @char in rawCommand)
            {
                if(@char == '\"')
                {
                    isAppendingRawString = !isAppendingRawString;
                    continue;
                }

                if(!isAppendingRawString && @char == ' ')
                {
                    substrings.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }
                sb.Append(@char);
            }

            substrings.Add(sb.ToString());
            sb.Clear();

            return T.Parse(substrings[1 + argumentIndex], null);
        }
        catch
        {
            throw new ArgumentException($"Could not parse argument {argumentIndex} of command {rawCommand}!");
        }
    }

    private T[] ParseArguments<T>(string rawCommand) where T : IParsable<T>
    {
        try
        {
            List<string> substrings = new();

            StringBuilder sb = new();
            bool isAppendingRawString = false;
            foreach(var @char in rawCommand)
            {
                if(@char == '\"')
                {
                    isAppendingRawString = !isAppendingRawString;
                    continue;
                }

                if(!isAppendingRawString && @char == ' ')
                {
                    substrings.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }
                sb.Append(@char);
            }

            substrings.Add(sb.ToString());
            sb.Clear();

            //first substring is command name
            T[] result = new T[substrings.Count - 1];

            for(int i = 0; i < result.Length; i++)
            {
                result[i] = T.Parse(substrings[i + 1], null);
            }

            return result;
        }
        catch
        {
            throw new ArgumentException($"Could not parse arguments of command {rawCommand}!");
        }
    }
}
