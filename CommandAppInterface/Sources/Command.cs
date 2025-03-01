﻿using System;

namespace CAI;

/// <summary>
/// User command to the app interface
/// </summary>
public class Command : IDescribable
{
    private string Name_, Description_, Usage_;

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
        Name_ = name;
        Description_ = description;
        Action = action;
        Usage_ = usage;
    }

    public string GetName()
    {        
            return Name_;        
    }
    public string GetDescription()
    {
        return Description_;
    }

    public string GetUsage()
    {
        return Usage_;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
public class Command<Argument1> : IDescribable where Argument1 : IParsable<Argument1>
{
    private string Name_, Description_, Usage_;
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
        Name_ = name;
        Description_ = description;
        Action = action;
        Usage_ = usage;
    }

    public string GetName()
    {
        return Name_;
    }
    public string GetDescription()
    {
        return Description_;
    }
    public string GetUsage()
    {
        return Usage_;

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
    private string Name_, Description_, Usage_;
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
        Name_ = name;
        Description_ = description;
        Action = action;
        Usage_ = usage;
    }
    public string GetName()
    {
        return Name_;
    }
    public string GetDescription()
    {
        return Description_;
    }
    public string GetUsage()
    {
        return Usage_;
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
    private string Name_, Description_, Usage_;
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
        Description_ = description;
        Action = action;
        Usage_ = usage;
    }

    public string GetName()
    {
        return Name_;
    }
    public string GetDescription()
    {
        return Description_;
    }
    public string GetUsage()
    {
        return Usage_;
    }
}

/// <summary>
/// User command to the app interface
/// </summary>
/// <typeparam name="Argument1">the first argument type</typeparam>
/// <typeparam name="Argument2">the second argument type</typeparam>
/// <typeparam name="Argument3">the third argument type</typeparam>
/// /// <typeparam name="Argument4">the fourth argument type</typeparam>
public class Command<Argument1, Argument2, Argument3, Argument4> : IDescribable
    where Argument1 : IParsable<Argument1>
    where Argument2 : IParsable<Argument2>
    where Argument3 : IParsable<Argument3>
    where Argument4 : IParsable<Argument4>
{
    private string Name_, Description_, Usage_;
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
        Name_ = name;
        Description_ = description;
        Action = action;
        Usage_ = usage;
    }

    public string GetName()
    {
        return Name_;
    }
    public string GetDescription()
    {
        return Description_;
    }
    public string GetUsage()
    {
        return Usage_;
    }
}