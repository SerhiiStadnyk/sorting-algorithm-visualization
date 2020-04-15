using UnityEngine;
using System;
using System.Reflection;

/// <summary>
/// Automatic ScriptableObject-Singleton system
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ScriptableObjectSingleton<T>: ScriptableObject where T: ScriptableObjectSingleton<T>
{
    public static string DefaultPath 
    {
        get 
        {
            string directory = "ScriptableObjects";
            string typeName = typeof(T).ToString();
            return directory + "/" + typeName;
        }
    }

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<T>(GetPathAttribute());
                Debug.LogWarning(GetPathAttribute());
            }
            return instance;
        }
    }

    private static string GetPathAttribute() 
    {
        Type t = typeof(T);

        var message = t.GetCustomAttribute(typeof(PathAttribute));

        if (message != null)
        {
            string directory = ((PathAttribute)message).path;
            string typeName = t.ToString();
            string result = directory + "/" + typeName;
            Debug.LogWarning($"Path:{directory}, Type:{t}");

            return result;
        }
        else
        {
            return DefaultPath;
        }
    }
}