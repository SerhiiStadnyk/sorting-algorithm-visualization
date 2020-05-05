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
    Last,
    Half,
    HalfReverse,
    Mirrored,
    MirroredReverse,
    Pyramid,
    PyramidReverse
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

    public int MaxFps { get; private set; }
    public int SortingTactsPerFrame { get; private set; }

    public void SetMaxFps(int value)
    {
        if (value < 5)
            MaxFps = 5;
        else
            MaxFps = value;

        Application.targetFrameRate = MaxFps;
    }

    public void SetSortingTactsPerFrame(int value)
    {
        if(value <= 0)
            SortingTactsPerFrame = 1;
        else if(value <= ArraySize)
            SortingTactsPerFrame = value;
        else
            SortingTactsPerFrame = ArraySize;
    }

    public void SetSortingType(int id) 
    {
        sortingType = (SortingTypes)id;
    }

    public void SetDelay(int value)
    {
        delay = value;
    }

    public void SetRandomizerType(int id)
    {
        randomizerType = (RandomizerTypes)id;
    }

    public void SetArraySize(int value)
    {
        if (value > MaxArraySize)
            value = MaxArraySize;
        else if (value < MinArraySize)
            value = MinArraySize;

        ArraySize = value;
    }

    public void SetMaximumArraySize(int value) 
    {
        MaxArraySize = value;
        SetArraySize(ArraySize);
    }
}