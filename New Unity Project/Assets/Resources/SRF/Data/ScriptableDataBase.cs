using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Path("ScriptableData")]
public abstract class ScriptableDataBase<T> : ScriptableObjectSingleton<T> where T : ScriptableDataBase<T>
{
    public static new string path = "ScriptableData";
}