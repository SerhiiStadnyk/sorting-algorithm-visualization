using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOperationMediator : MonoBehaviour, ISortingHandleable
{
    SortingAlgorithmBase sortingAlgorithm;
    //ArrayVisualizer arrayVisualizer;
    DataArray dataArray;

    public void StartSorting() 
    {
        sortingAlgorithm = SortingAlgorithmCreator.GetAlgorithm(this);
        sortingAlgorithm.StartSorting();
    }

    public void RelocateElements(int fromIndex, int toIndex) 
    {
        //arrayVisualizer.UpdateVisuals();
        dataArray.RelocateElements(fromIndex, toIndex);
        //arrayVisualizer.CompareVisuals(fromIndex, toIndex);
    }

    public void CompareElements(int firstElementIndex, int secondElementIndex) 
    {
        //arrayVisualizer.CompareVisuals(firstElementIndex, secondElementIndex);
    }

    public void FinishSorting()
    {
    }
}