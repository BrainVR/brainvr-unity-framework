using System;
using System.Reflection;

public class TypeHelper
{
    public static Type GetTypeByName(string name)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Name == name) return type;
            }
        }
        return null;
    }
}