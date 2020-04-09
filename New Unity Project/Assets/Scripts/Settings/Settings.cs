using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SortingTypes
{
    Bubble,
    Shaker,
    QuickSort
};

public enum RandomizerTypes
{
    Random,
    Inverse,
    Last
};

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/SpawnSettings", order = 1)]
public class Settings : ScriptableObjectSingleton<Settings>
{
    public SortingTypes SortingType { get; private set; }
    public RandomizerTypes RandomizerType { get; private set; }
    public int Delay { get; private set; }
    public int ArraySize { get; private set; }

    public void SetSortingType(int id) 
    {
        SortingType = (SortingTypes)id;
        Debug.Log($"Sorting type now is {SortingType}");
    }

    public void SetRandomizerType(int id)
    {
        RandomizerType = (RandomizerTypes)id;
        Debug.Log($"Randomizer type now is {RandomizerType}");
    }

    public void SetDelay(int value)
    {
        Delay = value;
        Debug.Log($"Delay now is {value}");
    }

    public void SetArraySize(int value)
    {
        ArraySize = value;
        Debug.Log($"Array size now is {value}");
    }
}