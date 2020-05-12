using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArrayVisualizerController))]
public class SortingController : MonoBehaviour, ISortingHandable
{
#pragma warning disable 0649
    [SerializeField] private Settings settings;
    [SerializeField] private SettingsView settingsView;
#pragma warning restore 0649

    private SortingAlgorithmBase sortingAlgorithm;
    private ArrayVisualizerController arrayVisualizer;
    private DataArray dataArray;

    private Coroutine sortingCoroutine;
    private Coroutine checkingCoroutine;

    public List<int> Array { get => dataArray.Array; set => dataArray.Array = value; }

    private void Awake()
    {
        dataArray = new DataArray();
        arrayVisualizer = GetComponent<ArrayVisualizerController>();
    }

    private void Start()
    {
        arrayVisualizer.Init(VisualizerTypes.Column, dataArray);

        settings.SetMaximumArraySize(arrayVisualizer.CalculateMaxArraySize());
        settingsView.arraySizeSlider.maxValue = settings.MaxArraySize;
        settingsView.arraySizeSlider.value = settings.ArraySize;
    }

    public void CreateArray()
    {
        dataArray.CreateArray(settings.ArraySize, settings.RandomizerType);
        arrayVisualizer.Build();
    }

    public void StartSorting()
    {
        if (dataArray.Array == null || dataArray.Array.Count == 0)
            return;

        CleanUp();
        sortingAlgorithm = SortingAlgorithmCreator.GetAlgorithm(this, settings.SortingType);

        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        if (sortingCoroutine != null)
            StopCoroutine(sortingCoroutine);
        sortingCoroutine = StartCoroutine(StateSorting());
    }
    private void CleanUp()
    {
        sortingAlgorithm = null;
        arrayVisualizer.RemoveMarks();
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        arrayVisualizer.UpdateElement(fromIndex);
        arrayVisualizer.UpdateElement(toIndex);
    }

    public void Button_CheckData()
    {
        CleanUp();
        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        checkingCoroutine = StartCoroutine(CheckData());
    }

    public void FinishSorting()
    {
        StopCoroutine(sortingCoroutine);

        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        checkingCoroutine = StartCoroutine(CheckData());
    }

    int counter = 1;
    private IEnumerator StateSorting()
    {
        foreach (int i in sortingAlgorithm.Sort()) 
        {
            if (counter >= settings.SortingTactsPerFrame)
            {
                counter = 1;
                arrayVisualizer.MarkElements();
                yield return new WaitForSeconds(settings.Delay / 1000f);
            }
            else 
            {
                counter++;
            }
        }
    }

    private IEnumerator CheckData() 
    {
        arrayVisualizer.RemoveMarks();
        for (int i = 0; i < dataArray.Array.Count; i++)
        {
            if (dataArray.Array[i] != i)
                arrayVisualizer.MarkForCheck(i, true);
            else
                arrayVisualizer.MarkForCheck(i, false);

            yield return new WaitForSeconds(settings.Delay / 1000f);
        }
    }

    public void MarkElements(params int[] markedElements)
    {
        arrayVisualizer.AddMarks(markedElements);
    }
}