
using Spectre.Console;

namespace CAI.Examples
{
    internal class Program
    {
        private const string MathExample = "math";
        private const string ExceptionExample = "exception";
        private const string CMDExample = "cmd";
        private static void Main(string[] args)
        {
            AppInterface exampleChoiceInterface = new(
                caiName: "examples hub",
                isCatchExceptions: true);

            exampleChoiceInterface.AddCommand<string>(
                new Command<string>("run", "run an example",(exampleName) => 
                { AnsiConsole.Clear(); Run(exampleName); }, $"\"run [{MathExample}/{ExceptionExample}/{CMDExample}]\"")
                );

            exampleChoiceInterface.Start();
        }

        private static void Run(string exampleName)
        {
            IExample example = null;
            switch(exampleName)
            {
                case MathExample:
                    example = new MathExample();
                    break;
                case ExceptionExample:
                    example = new ExceptionExample();
                    break;
                case CMDExample:
                    example = new CMDExample();
                    break;
                default:
                    break;
            }
            example.Run();
        }
    }
}
