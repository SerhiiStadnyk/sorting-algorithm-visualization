using System.Collections.Generic;
using UnityEngine;

public class SortingController : MonoBehaviour, ISortingHandleable
{
    private SortingAlgorithmBase sortingAlgorithm;
    [SerializeField] private ArrayVisualizerController arrayVisualizer;

    [SerializeField] private DataArray dataArray = null;
    [SerializeField] private Settings settings = null;

    private void Start()
    {
        dataArray = new DataArray();
        var tmpList = new List<int>();
        for (int i = 0; i < settings.ArraySize; i++)
        {
            tmpList.Add(i);
        }
        dataArray.SetupElements(tmpList);

        arrayVisualizer.Init(settings, dataArray);
    }

    public void StartSorting() 
    {
        sortingAlgorithm = SortingAlgorithmCreator.GetAlgorithm(this, settings.SortingType);
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
        sortingAlgorithm = null;
    }
}