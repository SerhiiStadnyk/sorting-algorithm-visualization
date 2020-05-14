using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Factory pattern for sorting algoritms
/// </summary>
public static class SortingAlgorithmCreator
{
    public static SortingAlgorithmBase GetAlgorithm(ISortingHandable handleable, SortingTypes sortingType)
    {
        SortingAlgorithmBase result = null;

        switch (sortingType)
        {
            case SortingTypes.Bubble:
                result = SortingAlgorithmCreator.CreateBubbleSorting(handleable);
                break;
            case SortingTypes.Shaker:
                result = SortingAlgorithmCreator.CreateShakerSorting(handleable);
                break;
            case SortingTypes.Insertion:
                result = SortingAlgorithmCreator.CreateInsertionSorting(handleable);
                break;
            case SortingTypes.InsertionBinary:
                result = SortingAlgorithmCreator.CreateInsertionBinarySorting(handleable);
                break;
        }
        return result;
    }

    private static SortingAlgorithmBase CreateBubbleSorting(ISortingHandable handleable)
    {
        return new BubbleSorting(handleable);
    }

    private static SortingAlgorithmBase CreateShakerSorting(ISortingHandable handleable)
    {
        return new ShakerSorting(handleable);
    }

    private static SortingAlgorithmBase CreateInsertionSorting(ISortingHandable handleable)
    {
        return new InsertionSorting(handleable);
    }

    private static SortingAlgorithmBase CreateInsertionBinarySorting(ISortingHandable handleable)
    {
        return new InsertionBinarySorting(handleable);
    }
}