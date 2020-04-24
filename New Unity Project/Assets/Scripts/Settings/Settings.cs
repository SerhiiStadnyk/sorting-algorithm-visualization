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
    Sorted,
    Random,
    Inverse,
    Last
};

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/SpawnSettings", order = 1)]
public class Settings : ScriptableObject
{
#pragma warning disable 0649
    [SerializeField] private SortingTypes sortingType;
    [SerializeField] private RandomizerTypes randomizerType;
    [SerializeField] private int delay;
    [SerializeField] private int minArraySize;
#pragma warning restore 0649
    public SortingTypes SortingType { get { return sortingType; } }
    public RandomizerTypes RandomizerType { get { return randomizerType; } }
    public int Delay { get { return delay; } }
    public int ArraySize { get; private set; }
    public int MaxArraySize { get; private set; }
    public int MinArraySize { get { return minArraySize; } }

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
        if (value > MaxArraySize)
            value = MaxArraySize;
        else if (value < MinArraySize)
            value = MinArraySize;

        ArraySize = value;
        //OnArraySizeChanged.Instance.Raise();

        Debug.Log($"Array size now is {value}");
    }

    public void SetMaximumArraySize(int value) 
    {
        MaxArraySize = value;
        SetArraySize(ArraySize);
    }
}