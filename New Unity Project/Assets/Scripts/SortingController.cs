using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(ArrayVisualizerController))]
public class SortingController : MonoBehaviour, ISortingHandable
{
#pragma warning disable 0649
    [SerializeField] private Settings settings;
    [SerializeField] private SettingsView settingsView;
    [SerializeField] private DataArray dataArray;

    [SerializeField] private TMP_Text arrayAccesInfo;
    [SerializeField] private TMP_Text comparesInfo;
    [SerializeField] private TMP_Text timeInfo;
#pragma warning restore 0649

    private SortingAlgorithmBase sortingAlgorithm;
    private ArrayVisualizerController arrayVisualizer;

    private Coroutine sortingCoroutine;
    private Coroutine checkingCoroutine;

    private List<int> tmpArray;

    public bool isSorting;

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
        if (isSorting)
            return;

        if (dataArray.Array == null || dataArray.Array.Count == 0)
            return;
        isSorting = true;
        tmpArray = new List<int>(dataArray.Array);

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

        arrayAccesInfo.text = "0";
        comparesInfo.text = "0";
        timeInfo.text = "0";
    }

    private void TextAddValue(TMP_Text text, float value = 1) 
    {
        float tmp = float.Parse(text.text);
        tmp += value;
        text.text = (tmp+ value).ToString();
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        TextAddValue(arrayAccesInfo);

        arrayVisualizer.UpdateElement(fromIndex);
        arrayVisualizer.UpdateElement(toIndex);
    }

    public void Button_CheckData()
    {
        if (isSorting)
            return;

        CleanUp();
        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        checkingCoroutine = StartCoroutine(CheckData());
    }

    public void ButtonReset() 
    {
        isSorting = false;

        if (checkingCoroutine != null)
            StopCoroutine(checkingCoroutine);
        if (sortingCoroutine != null)
            StopCoroutine(sortingCoroutine);

        dataArray.Array = tmpArray;
        CleanUp();

        arrayVisualizer.Build();
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
        float time = Time.time;
        foreach (int i in sortingAlgorithm.Sort()) 
        {
            if (counter >= settings.SortingTactsPerFrame)
            {
                if (!isSorting)
                    yield break;

                counter = 1;
                arrayVisualizer.MarkElements();
                yield return new WaitForSeconds(settings.Delay / 1000f);
                timeInfo.text = (Time.time - time).ToString();
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

        isSorting = false;
    }

    public void MarkElements(params int[] markedElements)
    {
        TextAddValue(comparesInfo, markedElements.Length);
        arrayVisualizer.AddMarks(markedElements);
    }
}