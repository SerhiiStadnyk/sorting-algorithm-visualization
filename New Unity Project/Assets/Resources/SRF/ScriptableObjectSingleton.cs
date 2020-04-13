using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

/// <summary>
/// Automatic ScriptableObject-Singleton system
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ScriptableObjectSingleton<T>: ScriptableObject where T: ScriptableObjectSingleton<T>
{
    public static string path;
    //public abstract string Path { get; }

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
                instance = Resources.Load<T>(GetPathReflection());
                Debug.LogWarning(GetPathReflection());
            }
            return instance;
        }
    }

    private static string GetPath() 
    {
        string result = null;

        //T fpp = new T();
        //path = fpp.Path;

        var foo = typeof(T).GetField("path");
        var bar = foo.GetValue(foo);
        //path = bar.ToString();
        //instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
        //instance = Resources.Load<T>(path);
        //Debug.LogWarning($"Path:{path}, Type:{typeof(T)}");

        return result;
    }

    private static string GetPathReflection() 
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