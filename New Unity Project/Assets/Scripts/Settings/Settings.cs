using ScriptableEvenetSystem;
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

    public void SetDelay(int value)
    {
        Delay = value;
        Debug.Log($"Delay now is {value}");
    }

    public void SetRandomizerType(int id)
    {
        RandomizerType = (RandomizerTypes)id;
        OnSetArrayRandomizerType.Instance.Raise();


        Debug.Log($"Randomizer type now is {RandomizerType}");
    }

    public void SetArraySize(int value)
    {
        ArraySize = value;
        OnArraySizeChanged.Instance.Raise();

        Debug.Log($"Array size now is {value}");
    }
}