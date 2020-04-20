using System;
using System.Reflection;
using UnityEngine;

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
            return directory;
        }
    }

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GetInstance();
            }
            return instance;
        }
    }

    private static T GetInstance() 
    {
        var instance = Resources.Load<T>(GetPathAttribute());

        var a = Resources.FindObjectsOfTypeAll(typeof(T));

        return instance;
    }

    private static string GetPathAttribute() 
    {
        Type t = typeof(T);

        var path = t.GetCustomAttribute(typeof(PathAttribute));

        if (path != null)
        {
            string directory = ((PathAttribute)path).path;
            string result = directory + "/" + t.Name;

            return result;
        }
        else
        {
            return DefaultPath;
        }
    }

    public static void CheckForInstanceEditor() 
    {
        // Feature in development
    }
}