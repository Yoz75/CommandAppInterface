
using Spectre.Console;

namespace CAI.Examples;

public class NoNameAndCopyrightExample : IExample
{
    public void Run()
    {
        AppInterface noNameAndCopyrightInterface = new("half clear interface", isCatchExceptions: true, style: InterfaceStyles.CopyrightMessage);
        noNameAndCopyrightInterface.AddCommand(new Command("foo", "just nothing", () => { AnsiConsole.WriteLine("foo!"); }, "\"foo\""));
        noNameAndCopyrightInterface.Start();
    }
}
