using ScriptableEvenetSystem;
using UnityEngine;
using UnityEditor;

public enum SortingTypes
{
    Bubble,
    Shaker,
    QuickSort
};

public enum RandomizerTypes
{
    Sorted,
    Random,
    Inverse,
    Last
};

[InitializeOnLoad]
[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/SpawnSettings", order = 1)]
public class Settings : ScriptableObject
{
    [SerializeField] private SortingTypes sortingType;
    [SerializeField] private RandomizerTypes randomizerType;
    [SerializeField] private int delay;
    [SerializeField] private int arraySize;
    public SortingTypes SortingType { get { return sortingType; } }
    public RandomizerTypes RandomizerType { get { return randomizerType; } }
    public int Delay { get { return delay; } }
    public int ArraySize { get { return arraySize; } }

    public void SetSortingType(int id) 
    {
        sortingType = (SortingTypes)id;
        Debug.Log($"Sorting type now is {sortingType}");
    }

    public void SetDelay(int value)
    {
        delay = value;
        Debug.Log($"Delay now is {value}");
    }

    public void SetRandomizerType(int id)
    {
        randomizerType = (RandomizerTypes)id;
        OnSetArrayRandomizerType.Instance.Raise();


        Debug.Log($"Randomizer type now is {randomizerType}");
    }

    public void SetArraySize(int value)
    {
        arraySize = value;
        OnArraySizeChanged.Instance.Raise();

        Debug.Log($"Array size now is {value}");
    }
}