using System;

namespace CAI;

/// <summary>
/// User command to the app interface
/// </summary>
public class Command : IDescribable
{
    private string Name, Description, Usage;

    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {        
            return Name;        
    }
    public string GetDescription()
    {
        return Description;
    }

    public string GetUsage()
    {
        return Usage;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
public class Command<Argument1> : IDescribable where Argument1 : IParsable<Argument1>
{
    private string Name, Description, Usage;
    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument1> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action<Argument1> action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }
    public string GetUsage()
    {
        return Usage;

    } 
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
/// <typeparam name="Argument2">the second argument type</typeparam>
public class Command<Argument1, Argument2> : IDescribable
where Argument1 : IParsable<Argument1>
where Argument2 : IParsable<Argument2>
{
    private string Name, Description, Usage;
    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument1, Argument2> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action<Argument1, Argument2> action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }
    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }
    public string GetUsage()
    {
        return Usage;
    }
}

    /// <summary>
    /// User command to the app interface
    /// </summary>
    /// <typeparam name="Argument1">the first argument type</typeparam>
    /// <typeparam name="Argument2">the second argument type</typeparam>
    /// <typeparam name="Argument3">the third argument type</typeparam>
    public class Command<Argument1, Argument2, Argument3> : IDescribable
    where Argument1 : IParsable<Argument1>
    where Argument2 : IParsable<Argument2>
    where Argument3 : IParsable<Argument3>
{
    private string Name_, Description, Usage;
    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument1, Argument2, Argument3> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action<Argument1, Argument2, Argument3> action, string? usage = null)
    {
        Name_ = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {
        return Name_;
    }
    public string GetDescription()
    {
        return Description;
    }
    public string GetUsage()
    {
        return Usage;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
/// <typeparam name="Argument2">the second argument type</typeparam>
/// <typeparam name="Argument3">the third argument type</typeparam>
/// <typeparam name="Argument4">the fourth argument type</typeparam>
public class Command<Argument1, Argument2, Argument3, Argument4> : IDescribable
    where Argument1 : IParsable<Argument1>
    where Argument2 : IParsable<Argument2>
    where Argument3 : IParsable<Argument3>
    where Argument4 : IParsable<Argument4>
{
    private string Name, Description, Usage;
    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument1, Argument2, Argument3, Argument4> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action<Argument1, Argument2, Argument3, Argument4> action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }
    public string GetUsage()
    {
        return Usage;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
/// <typeparam name="Argument2">the second argument type</typeparam>
/// <typeparam name="Argument3">the third argument type</typeparam>
/// <typeparam name="Argument4">the fourth argument type</typeparam>
/// <typeparam name="Argument5">the fifth argument type</typeparam>
public class Command<Argument1, Argument2, Argument3, Argument4, Argument5> : IDescribable
    where Argument1 : IParsable<Argument1>
    where Argument2 : IParsable<Argument2>
    where Argument3 : IParsable<Argument3>
    where Argument4 : IParsable<Argument4>
    where Argument5 : IParsable<Argument5>
{
    private string Name, Description, Usage;
    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument1, Argument2, Argument3, Argument4, Argument5> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action<Argument1, Argument2, Argument3, Argument4, Argument5> action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }
    public string GetUsage()
    {
        return Usage;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
/// <typeparam name="Argument2">the second argument type</typeparam>
/// <typeparam name="Argument3">the third argument type</typeparam>
/// <typeparam name="Argument4">the fourth argument type</typeparam>
/// <typeparam name="Argument5">the fifth argument type</typeparam>
/// <typeparam name="Argument6">the sixth argument type</typeparam>
public class Command<Argument1, Argument2, Argument3, Argument4, Argument5, Argument6> : IDescribable
    where Argument1 : IParsable<Argument1>
    where Argument2 : IParsable<Argument2>
    where Argument3 : IParsable<Argument3>
    where Argument4 : IParsable<Argument4>
    where Argument5 : IParsable<Argument5>
    where Argument6 : IParsable<Argument6>
{
    private string Name, Description, Usage;
    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument1, Argument2, Argument3, Argument4, Argument5, Argument6> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public Command(string name, string description, Action<Argument1, Argument2, Argument3, Argument4, Argument5, Argument6> action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }
    public string GetUsage()
    {
        return Usage;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// /// <typeparam name="Argument">the type of array of arguments</typeparam>

public class VariableArgsCommand<Argument> : IDescribable
{
    private string Name, Description, Usage;

    /// <summary>
    /// Action that app interface execute when receive this command
    /// </summary>
    public Action<Argument[]> Action
    {
        get;
        private set;
    }

    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="action">Action that app interface execute when receive this command</param>
    public VariableArgsCommand(string name, string description, Action<Argument[]> action, string? usage = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Usage = usage;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }

    public string GetUsage()
    {
        return Usage;
    }
}