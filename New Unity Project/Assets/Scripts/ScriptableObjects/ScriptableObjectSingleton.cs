using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Automatic ScriptableObject-Singleton system
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ScriptableObjectSingleton<T>: ScriptableObject where T: ScriptableObject
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null) 
            {
                instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            }
            return instance;
        }
    }
}