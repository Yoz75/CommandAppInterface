
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace CAI.Reflection;

public static class AppInteraceExtensions
{
    private static List<Type> CachedCommandTypes = new();
    private static List<Type> CachedActionTypes = new();
    /// <summary>
    /// Load all comands that are attributed with <see cref="CommandAttribute"/> 
    /// and are intended for this app interface.
    /// </summary>
    public static void LoadAttributedCommands(this AppInterface appInterface)
    {
        foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach(var type in assembly.GetTypes())
            {
                foreach(var method in type.GetMethods())
                {
                    var commandAttribute = method.GetCustomAttribute<CommandAttribute>();
                    if(commandAttribute == null) continue;

                    if(commandAttribute != null && commandAttribute.CAIName == appInterface.Name)
                    {
                        appInterface.AddByMethodInfo(method, commandAttribute);
                    }
                }
            }
        }
    }

    private static void AddByMethodInfo(this AppInterface appInterface,
        MethodInfo method, CommandAttribute attribute)
    {
        TryCacheTypesOf(typeof(Command), CachedCommandTypes);
        TryCacheTypesOf(typeof(Action), CachedActionTypes, "System");

        var parameters = method.GetParameters();
        var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();

        string name = attribute.Name;
        string description = attribute.Description;
        string usage = attribute.Usage;

        Type targetCommandType = CachedCommandTypes.FirstOrDefault(t =>
                (t.IsGenericTypeDefinition ? t.GetGenericArguments().Length : 0) == parameterTypes.Length);

        Type targetActionType = CachedActionTypes.FirstOrDefault(t =>
        (t.IsGenericTypeDefinition ? t.GetGenericArguments().Length : 0) == parameterTypes.Length);

        if(targetCommandType == null)
            throw new InvalidOperationException(
                $"No command type found  with {parameterTypes.Length} parameters.");

        if(targetActionType.IsGenericTypeDefinition)
            targetActionType = targetActionType.MakeGenericType(parameterTypes);

        var methodDelegate = method.CreateDelegate(targetActionType);

        if(targetCommandType.IsGenericTypeDefinition)
            targetCommandType = targetCommandType.MakeGenericType(parameterTypes);

        object commandInstance = Activator.CreateInstance(targetCommandType, name, description, methodDelegate, usage);

        var addCommandParameters = new List<Type>(parameterTypes.Length);

        MethodInfo addCommandMethod = null;
        if(targetCommandType.IsGenericType)
            addCommandMethod = GetMethodWithGenericParams(typeof(AppInterface), parameterTypes, "AddCommand");
        else
            addCommandMethod = GetNonGenericMethod(typeof(AppInterface), "AddCommand");
        addCommandMethod.Invoke(appInterface, [commandInstance]);
    }

    /// <summary>
    /// Try to cache all generic types of baseType (including itself)
    /// </summary>
    /// <param name="baseType"></param>
    /// <param name="cache"></param>
    private static void TryCacheTypesOf(Type baseType, List<Type> cache, string targetNamespace = "")
    {
        if(cache.Count != 0) return;

        if(targetNamespace != "")
        {
            string genericsName = baseType.Name + "`";

            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(var type in assembly.GetTypes())
                {

                    if(type.Name != baseType.Name && !type.Name.StartsWith(genericsName))
                        continue;

                    if(type.Namespace != targetNamespace)
                        continue;

                    cache.Add(type);
                }
            }
        }
        else
        {
            string genericsName = baseType.Name + "`";

            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(var type in assembly.GetTypes())
                {
                    if(type.Name != baseType.Name && !type.Name.StartsWith(genericsName))
                        continue;
                    cache.Add(type);
                }
            }
        }
    }

    private static MethodInfo GetMethodWithGenericParams(Type type, Type[] paramsTypes, string nameStart)
    {
        var allMethods = type.GetMethods();

        foreach(var method in allMethods)
        {
            if(method.GetGenericArguments().Length == paramsTypes.Length && method.Name.StartsWith(nameStart))
            {
                return method.MakeGenericMethod(paramsTypes);
            }
        }

        return null;
    }

    private static MethodInfo GetNonGenericMethod(Type type, string nameStart)
    {
        var allMethods = type.GetMethods();

        foreach(var method in allMethods)
        {
            if(method.GetGenericArguments().Length == 0 && method.Name.StartsWith(nameStart))
            {
                return method;
            }
        }

        return null;
    }
}
