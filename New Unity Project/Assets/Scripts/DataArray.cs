using System.Collections.Generic;
using UnityEngine;

public class DataArray
{
    public List<int> Array { get; private set; }
    public void RelocateElements(int fromIndex, int toIndex) 
    {
        int tmp = Array[fromIndex];
        Array[fromIndex] = toIndex;
        Array[toIndex] = tmp;
    }

    public void SetupElements(List<int> array) 
    {
        Array = array;
    }

    public void CreateArray(int arraySize, RandomizerTypes randomizerType) 
    {
        switch (randomizerType)
        {
            case RandomizerTypes.Sorted:
                SetupElements(CreateArraySorted(arraySize));
                break;
            case RandomizerTypes.Random:
                SetupElements(CreateArrayRandom(arraySize));
                break;
            case RandomizerTypes.Inverse:
                SetupElements(CreateArrayInverted(arraySize));
                break;
            case RandomizerTypes.Last:
                SetupElements(CreateArrayLast(arraySize));
                break;
            default:
                SetupElements(CreateArraySorted(arraySize));
                break;
        }
    }

    private List<int> CreateArraySorted(int arraySize) 
    {
        var tmpList = new List<int>();
        for (int i = 0; i < arraySize; i++)
        {
            tmpList.Add(i);
        }

        return tmpList;
    }

    private List<int> CreateArrayRandom(int arraySize)
    {
        var resultList = new List<int>();
        var tmpList = new List<int>();
        for (int i = 0; i < arraySize; i++)
        {
            tmpList.Add(i);
        }

        for (int i = 0; i < arraySize; i++)
        {
            int randomIndex = Random.Range(0, tmpList.Count - 1);
            Debug.Log(tmpList.Count);
            Debug.Log(randomIndex);
            int randomValue = tmpList[randomIndex];
            resultList.Add(randomValue);
            tmpList.RemoveAt(randomIndex);
        }

        return resultList;
    }

    private List<int> CreateArrayInverted(int arraySize)
    {
        var tmpList = new List<int>();
        for (int i = arraySize; i >= 0; i--)
        {
            tmpList.Add(i);
        }

        return tmpList;
    }

    private List<int> CreateArrayLast(int arraySize)
    {
        var tmpList = new List<int>();
        for (int i = 0; i < arraySize; i++)
        {
            tmpList.Add(i);
        }
        int tmp = tmpList[0];
        tmpList[0] = tmpList[tmpList.Count - 1];
        tmpList[tmpList.Count - 1] = tmp;

        return tmpList;
    }
}