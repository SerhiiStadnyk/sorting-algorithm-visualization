using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArrayVisualizerController))]
public class SortingController : MonoBehaviour, ISortingHandleable
{
    [SerializeField] private DataArray dataArray = null;
    [SerializeField] private Settings settings = null;

    private SortingAlgorithmBase sortingAlgorithm;
    private ArrayVisualizerController arrayVisualizer;

    private void Start()
    {
        arrayVisualizer = GetComponent<ArrayVisualizerController>();
        arrayVisualizer.Init(dataArray);
        settings.SetMaximumArraySize(arrayVisualizer.CalculateMaxArrayNumber());
        dataArray = new DataArray();
        var tmpList = new List<int>();
        for (int i = 0; i < settings.ArraySize; i++)
        {
            tmpList.Add(i);
        }
        dataArray.SetupElements(tmpList);
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