﻿
using Spectre.Console;

namespace CAI.Examples;

class MathExample : IExample
{
    public void Run()
    {
        AppInterface mathInterface = new(caiName: "calculator example", isCatchExceptions: true);
        mathInterface.AddCommand<double, double>(new Command<double, double>("add", "add 2 numbers", Add, "\"add [num1] [num2]\""));
        mathInterface.AddCommand<double, double>(new Command<double, double>("sub", "subdivide 2 numbers", Subdivide, "\"sub [num1] [num2]\""));
        mathInterface.AddCommand<double, double>(new Command<double, double>("mul", "multiply 2 numbers", Multiply, "\"mul [num1] [num2]\""));
        mathInterface.AddCommand<double, double>(new Command<double, double>("div", "divide 2 numbers", Divide, "\"div [num1] [num2]\""));

        mathInterface.Start();
    }

    public void Add(double number1, double number2)
    {
        AnsiConsole.WriteLine(number1 + number2);
    }

    public void Subdivide(double number1, double number2)
    {
        AnsiConsole.WriteLine(number1 - number2);
    }

    public void Multiply(double number1, double number2)
    {
        AnsiConsole.WriteLine(number1 * number2);
    }

    public void Divide(double number1, double number2)
    {
        AnsiConsole.WriteLine(number1 / number2);
    }
}
