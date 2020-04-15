using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[InitializeOnLoad]
class ScriptableController
{
    static ScriptableController()
    {
        //CheckForAllScriptableSingletons();
    }

    static void CheckForAllScriptableSingletons() 
    {
        var objects = Resources.LoadAll("SRF");

        for (int i = 0; i < objects.Length; i++)
        {
            Type t = objects.GetType();
            var message = t.GetCustomAttributes();
            foreach (var mes in message) { Debug.LogError(mes); }
            if(message != null)
                Debug.Log(objects[i].name);
            else
                Debug.LogWarning("Ignored: " + objects[i].name);

            Type type = Type.GetType(objects[i].name);
            //ScriptableObject types = objects[i] as type;

            // Тип через стринг, имя класса

            //Debug.LogError(objects[i].GetType());

            //var someClass = objects[i] as Settings;
            //if(someClass != null)
            //    Debug.Log(objects[i].name);
            //else
            //    Debug.LogWarning("Ignored: " + objects[i].name);

        }
        Debug.LogWarning("Cheking is over");
    }
}