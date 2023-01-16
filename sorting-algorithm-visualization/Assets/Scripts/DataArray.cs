using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataArray
{
    public List<int> Array { get; set; }
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
            case RandomizerTypes.Half:
                SetupElements(CreateArrayHalf(arraySize));
                break;
            case RandomizerTypes.HalfReverse:
                SetupElements(CreateArrayHalfReverse(arraySize));
                break;
            case RandomizerTypes.Mirrored:
                SetupElements(CreateArrayMirrored(arraySize));
                break;
            case RandomizerTypes.MirroredReverse:
                SetupElements(CreateArrayMirroredReverse(arraySize));
                break;
            case RandomizerTypes.Pyramid:
                SetupElements(CreateArrayPyramid(arraySize));
                break;
            case RandomizerTypes.PyramidReverse:
                SetupElements(CreateArrayPyramidReverse(arraySize));
                break;
            default:
                SetupElements(CreateArraySorted(arraySize));
                break;
        }
    }

    private List<int> CreateArraySorted(int arraySize, int startIndex = 0) 
    {
        arraySize = arraySize + startIndex;

        var tmpList = new List<int>();
        for (int i = startIndex; i < arraySize; i++)
        {
            tmpList.Add(i);
        }

        return tmpList;
    }

    private List<int> CreateArrayRandom(int arraySize, int startIndex = 0)
    {
        arraySize = arraySize + startIndex;

        var resultList = new List<int>();
        var tmpList = new List<int>();
        for (int i = startIndex; i < arraySize; i++)
        {
            tmpList.Add(i);
        }

        for (int i = startIndex; i < arraySize; i++)
        {
            int randomIndex = Random.Range(0, tmpList.Count - 1);
            int randomValue = tmpList[randomIndex];
            resultList.Add(randomValue);
            tmpList.RemoveAt(randomIndex);
        }

        return resultList;
    }

    private List<int> CreateArrayInverted(int arraySize, int startIndex = 0)
    {
        var tmpList = new List<int>();
        for (int i = arraySize; i >= startIndex; i--)
        {
            tmpList.Add(i);
        }

        return tmpList;
    }

    private List<int> CreateArrayLast(int arraySize)
    {
        var tmpList = new List<int>();

        if (arraySize < 1) 
        {
            Debug.LogError("Incorrect array size");
            return tmpList;
        }

        for (int i = 0; i < arraySize; i++)
        {
            tmpList.Add(i);
        }
        int tmp = tmpList[0];
        tmpList[0] = tmpList[tmpList.Count - 1];
        tmpList[tmpList.Count - 1] = tmp;

        return tmpList;
    }

    private List<int> CreateArrayHalf(int arraySize)
    {
        var tmpList = CreateArraySorted((int)(arraySize * 0.5f));
        var randomList = CreateArrayRandom(Mathf.CeilToInt(arraySize * 0.5f), (int)(arraySize*0.5f));
        randomList.ForEach(element => tmpList.Add(element));

        return tmpList;
    }

    private List<int> CreateArrayHalfReverse(int arraySize)
    {
        var tmpList = CreateArraySorted(Mathf.CeilToInt(arraySize * 0.5f), (int)(arraySize * 0.5f));
        var randomList = CreateArrayRandom((int)(arraySize * 0.5f));
        tmpList.ForEach(element => randomList.Add(element));

        return randomList;
    }

    private List<int> CreateArrayMirrored(int arraySize)
    {
        var tmpList = CreateArraySorted((int)(arraySize * 0.5f));
        var invertList = CreateArrayInverted(arraySize - 1, (int)(arraySize * 0.5f));
        invertList.ForEach(element => tmpList.Add(element));

        return tmpList;
    }

    private List<int> CreateArrayMirroredReverse(int arraySize)
    {
        var tmpList = CreateArrayMirrored(arraySize);
        tmpList.Reverse();

        return tmpList;
    }

    private List<int> CreateArrayPyramid(int arraySize)
    {
        var tmpList = new List<int>();
        int offset = 0;
        if (arraySize % 2 != 0)
            offset = 1;

        for (int i = 0; i < arraySize; i+= 2)
        {
            tmpList.Add(i);
        }
        for (int i = arraySize - 1 - offset; i > 0; i -= 2)
        {
            tmpList.Add(i);
        }

        return tmpList;
    }

    private List<int> CreateArrayPyramidReverse(int arraySize)
    {
        var tmpList = new List<int>();
        int offset = 0;
        if (arraySize % 2 != 0)
            offset = 1;

        for (int i = arraySize - 1 - offset; i > 0; i -= 2)
        {
            tmpList.Add(i);
        }
        for (int i = 0; i < arraySize; i += 2)
        {
            tmpList.Add(i);
        }

        return tmpList;
    }
}