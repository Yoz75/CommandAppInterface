NuGet: https://www.nuget.org/packages/CommandAppInterface/
# Command App Interface
This is a library for easy command interface making (actualy I used something like this in my other projects, but I tired of always typing if/else):<br>
![изображение](https://github.com/user-attachments/assets/133b3b77-6dfe-4be3-999d-99cb4f648d72)
(you can see this example in CAIExamples folder)

## Usage:
First you need to use CAI namespace and create CAI instance:
```cs
AppInterface interface = new(caiName: "example", isCatchExceptions: true);
```
If you want to handle exceptions yourself, set isCatchExceptions false.
Next to create a command, use AppInterface.AddCommand method:
```cs
interface.AddCommand(new Command("command_name", "this is a command without parameters", DoSomeDealMethod, "this is text about how to use this command);

interface.AddCommand<int>(new Command("command_with_int_parameter", "this command expects an int parameter after command name", MethodWithIntArgument, "usage: \"command_with_int_parameter 42\""));
```
Now commands support up to 6 various parameters. Each parameters implements IParsable.<br>
In the command constructor, the command handler method is assigned. It must have the same number of parameters and the same types as those specified in the command generics<br>

CAI supports commands with variable parameters count:
```cs
interface.AddVariableArgsCommand<double>("command_with_many_double_parameters". "this command expects any count of doubles", DoSomeDealWithDoubleArray, "usage: \"command_with_many_double_parameters num1 num2 ... numN\"")
```

To start interface, run AppInterface.Start() method:
```cs
interface.Start();
```
The whole code looks like this:
```cs
...
AppInterface interface = new(caiName: "example", isCatchExceptions: true);

interface.AddCommand(new Command("command_name", "this is a command without parameters", DoSomeDealMethod, "this is text about how to use this command);
interface.AddCommand<int>(new Command("command_with_int_parameter", "this command expects an int parameter after command name", MethodWithIntArgument, "usage: \"command_with_int_parameter 42\""));
interface.AddVariableArgsCommand<double>("command_with_many_double_parameters". "this command expects any count of doubles", DoSomeDealWithDoubleArray, "usage: \"command_with_many_double_parameters num1 num2 ... numN\"")

interface.Start();
...
```
Also CAI contains built-in commands:
![изображение](https://github.com/user-attachments/assets/77bf846e-32cb-4d41-8e87-5ada6e983405)

You can run one CAI inside other. In this case when you type quit, you will exit from internal CAI and continue working in external CAI:
![изображение](https://github.com/user-attachments/assets/d8e47820-c742-4595-a31e-b49052516191)

## Logging
CAI can log your string message, or any Spectre.Console.Rendering.IRenderable:<br>
![изображение](https://github.com/user-attachments/assets/3d058d7e-bd5d-4dd2-97b4-7d9c4d3a52cb)


## Styles
CAI supports several styles:
* None -- no welcome message and no command prompt
* WriteCommandPrompt -- only command prompt
* WelcomeMessage -- only welcome message
* WelcomeAndCommandPrompt -- both command prompt and welcome message
* CopyrightMessage -- Spectre.Console copyright
* CAINAme -- show CAI name at start
* Full -- standard CAI style. Contains everything
this way you can make clear interface without welcome message or blue command prompt:
```cs
            AppInterface clearInterface = new("clear interface", isCatchExceptions: true, style: InterfaceStyles.None);
```
"style" parameter isn`t required, it is set to Styles.Full by default.
Styles are presented enum with Flags attribute, so you can actually use WelcomeMessage | WriteCommandPromt instead of WelcomeAndCommandPrompt1.

## Native AOT:
CAI uses [Spectre.Console](https://github.com/spectreconsole/spectre.console/)  (Not Spectre.Console.Cli!!!), as stated in https://spectreconsole.net/best-practices, Spectre.Console supports Native AOT.
