using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataArray: ScriptableObjectSingleton<DataArray>
{
    public List<int> Array { get; private set; }

    public void RelocateElements(int fromIndex, int toIndex) { }

    public void SetupElements(List<int> array) 
    {
        Array = array;
    }

}