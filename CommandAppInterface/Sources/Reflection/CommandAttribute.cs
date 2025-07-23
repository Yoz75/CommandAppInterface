
namespace CAI.Reflection;

[System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class CommandAttribute : System.Attribute
{
    public string Name { get; }
    public string Description { get; }
    public string Usage { get; }
    public string CAIName { get; }

    /// <summary>
    /// Create a new command attribute.
    /// </summary>
    /// <param name="name">command name</param>
    /// <param name="caiName">CAI name this command will appear only in app interface, named this name</param>
    /// <param name="description">command description</param>
    public CommandAttribute(string caiName, string name, string description = "", string usage = "")
    {
        Name = name;
        CAIName = caiName;
        Description = description;
        Usage = usage;
    }
}
