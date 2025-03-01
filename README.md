# Command App Interface
This is a library for easy command interface making:
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
Now commands support up to 4 various parameters. Each parameters implements IParsable.<br>
In the command constructor, the command handler method is assigned. It must have the same number of parameters and the same types as those specified in the command generics
