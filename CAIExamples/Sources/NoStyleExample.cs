using Spectre.Console;

namespace CAI.Examples
{
    public class NoStyleExample : IExample
    {
        public void Run()
        {
            AppInterface clearInterface = new("clear interface", isCatchExceptions: true, style: InterfaceStyles.None);
            clearInterface.AddCommand(new Command("foo", "just nothing", () => { AnsiConsole.WriteLine("foo!"); }, "\"foo\""));
            clearInterface.Start();
        }
    }
}
