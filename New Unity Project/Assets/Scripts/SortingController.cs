using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArrayVisualizerController))]
public class SortingController : MonoBehaviour, ISortingHandleable
{
#pragma warning disable 0649
    [SerializeField] private Settings settings;
    [SerializeField] private SettingsView settingsView;
#pragma warning restore 0649

    private SortingAlgorithmBase sortingAlgorithm;
    private ArrayVisualizerController arrayVisualizer;
    private DataArray dataArray;

    private void Awake()
    {
        dataArray = new DataArray();
        arrayVisualizer = GetComponent<ArrayVisualizerController>();
    }

    private void Start()
    {
        settings.SetMaximumArraySize(arrayVisualizer.CalculateMaxArrayNumber());
        settingsView.arraySizeSlider.maxValue = settings.MaxArraySize;
        settingsView.arraySizeSlider.value = settings.ArraySize;
    }

    public void CreateArray() 
    {
        dataArray.CreateArray(settings.ArraySize, settings.RandomizerType);
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