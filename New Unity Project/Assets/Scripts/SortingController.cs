using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        //Application.targetFrameRate = 60;
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
        CleanUp();
        sortingAlgorithm = SortingAlgorithmCreator.GetAlgorithm(this, settings.SortingType);

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

    public void FinishSorting() { }

    public void Button_CheckData()
    {
        CleanUp();
        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        checkingCoroutine = StartCoroutine(CheckData());
    }

    private IEnumerator StateSorting()
    {
        while (sortingAlgorithm.IsSorted == false)
        {
            yield return null;
            for (int i = 0; i < 1; i++)
            {
                sortingAlgorithm.SortingStep();
            }
            arrayVisualizer.MarkElements();
        }

        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        checkingCoroutine = StartCoroutine(CheckData());
    }

    private IEnumerator CheckData() 
    {
        bool isWrong = false;
        arrayVisualizer.RemoveMarks();
        for (int i = 0; i < dataArray.Array.Count; i++)
        {
            if (i != 0)
            {
                if (dataArray.Array[i] < dataArray.Array[i - 1])
                    isWrong = true;

                arrayVisualizer.MarkForCheck(i, isWrong);
            }
            else
            {
                arrayVisualizer.MarkForCheck(i, isWrong);
            }

            yield return null;
        }
    }

    public void MarkElements(params int[] markedElements)
    {
        arrayVisualizer.AddMarks(markedElements);
    }
}