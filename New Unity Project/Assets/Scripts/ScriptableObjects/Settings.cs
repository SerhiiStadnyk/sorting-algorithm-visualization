using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SortingType 
{
    Bubble,
    Shaker,
    QuickSort
};

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/SpawnSettings", order = 1)]
public class Settings : ScriptableObjectSingleton<Settings>
{
    public SortingType SortingType { get; private set; }
}
