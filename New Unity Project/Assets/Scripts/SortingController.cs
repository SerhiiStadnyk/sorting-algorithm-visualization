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

    private List<IEnumerator> replacerCorountines = new List<IEnumerator>();
    private List<IEnumerator> checkerCorountines = new List<IEnumerator>();

    public List<int> Array { get => dataArray.Array; set => dataArray.Array = value; }

    private void Awake()
    {
        Application.targetFrameRate = 300;
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
        sortingAlgorithm.StartSorting();
    }
    private void CleanUp() 
    {
        sortingAlgorithm = null;
        replacerCorountines.Clear();
        checkerCorountines.Clear();
        arrayVisualizer.RemoveMarks();
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        int index = replacerCorountines.Count;
        replacerCorountines.Add(ReplaceElementsWaiter(fromIndex, toIndex, index));

        //arrayVisualizer.UpdateVisuals();
        //dataArray.RelocateElements(fromIndex, toIndex);
        //arrayVisualizer.CompareVisuals(fromIndex, toIndex);
    }

    public void CompareElements(int firstElementIndex, int secondElementIndex)
    {
        //arrayVisualizer.CompareVisuals(firstElementIndex, secondElementIndex);
    }

    public void FinishSorting()
    {
        CheckData();
        if (replacerCorountines.Count > 0)
            StartCoroutine(replacerCorountines[0]);
        if (checkerCorountines.Count > 0)
            StartCoroutine(checkerCorountines[0]);
    }

    public void Button_CheckData() 
    {
        CleanUp();
        FinishSorting();
    }

    private void CheckData() 
    {
        bool isWrong = false;
        arrayVisualizer.RemoveMarks();
        for (int i = 0; i < dataArray.Array.Count; i++)
        {
            if (i != 0)
            {
                if (dataArray.Array[i] < dataArray.Array[i - 1])
                    isWrong = true;
                checkerCorountines.Add(Waiter(i, isWrong));
            }
            else 
            {
                checkerCorountines.Add(Waiter(i, isWrong));
            }
        }
    }

    IEnumerator ReplaceElementsWaiter(int fromIndex, int toIndex, int i)
    {
        arrayVisualizer.MarkMainElemet(fromIndex);
        arrayVisualizer.MarkSecondaryElemet(toIndex);
        yield return null;
        arrayVisualizer.SwitchElements(fromIndex, toIndex);
        if (i + 1 < replacerCorountines.Count)
        {
            StartCoroutine(replacerCorountines[i + 1]);
        }
        else 
        {
            replacerCorountines.Clear();
        }
    }

    IEnumerator Waiter(int i, bool isWrong) 
    {
        while(replacerCorountines.Count > 0)
            yield return null;

        yield return null;
        arrayVisualizer.MarkForCheck(i, isWrong);
        if(i + 1 < checkerCorountines.Count)
            StartCoroutine(checkerCorountines[i + 1]);
    }
}