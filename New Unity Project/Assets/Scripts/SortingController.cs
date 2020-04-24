using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArrayVisualizerController))]
public class SortingController : MonoBehaviour, ISortingHandleable
{
#pragma warning disable 0649
    [SerializeField] private DataArray dataArray;
    [SerializeField] private Settings settings;
    [SerializeField] private SettingsView settingsView;
#pragma warning restore 0649

    private SortingAlgorithmBase sortingAlgorithm;
    private ArrayVisualizerController arrayVisualizer;

    private void Awake()
    {
        dataArray = new DataArray();
        arrayVisualizer = GetComponent<ArrayVisualizerController>();
    }

    private void Start()
    {
        settings.SetMaximumArraySize(arrayVisualizer.CalculateMaxArrayNumber());
        settingsView.arraySizeSlider.maxValue = settings.MaxArraySize;
    }

    public void CreateArray() 
    {
        var tmpList = new List<int>();
        for (int i = 0; i < settings.ArraySize; i++)
        {
            tmpList.Add(i);
        }
        dataArray.SetupElements(tmpList);
        arrayVisualizer.Init(dataArray);
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