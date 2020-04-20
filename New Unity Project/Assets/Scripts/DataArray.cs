using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataArray", menuName = "ScriptableObjects/SpawnDataArray", order = 1)]
public class DataArray: ScriptableObject
{
    public List<int> Array { get; private set; }

    public void RelocateElements(int fromIndex, int toIndex) { }

    public void SetupElements(List<int> array) 
    {
        Array = array;
    }
}