using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Factory pattern for sorting algoritms
/// </summary>
public static class SortingAlgorithmCreator
{
    public static SortingAlgorithmBase GetAlgorithm(ISortingHandleable handleable)
    {
        SortingAlgorithmBase result = null;

        switch (Settings.Instance.SortingType)
        {
            case SortingType.Bubble:
                result = SortingAlgorithmCreator.CreateBubbleSorting(handleable);
                break;
            case SortingType.Shaker:
                break;
            case SortingType.QuickSort:
                break;
        }
        return result;
    }

    private static SortingAlgorithmBase CreateBubbleSorting(ISortingHandleable handleable)
    {
        return new BubbleSorting(handleable);
    }
}