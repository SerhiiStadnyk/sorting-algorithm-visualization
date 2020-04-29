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

    private int waiting = 0;

    public List<int> Array { get => dataArray.Array; set => dataArray.Array = value; }

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
        int index = replacerCorountines.Count;
        replacerCorountines.Add(ReplaceElementsWaiter(fromIndex, toIndex, index));
        Debug.Log("Relocation added");

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
        Debug.Log("Relocations overall " + replacerCorountines.Count);
        arrayVisualizer.RemoveMarks();
        for (int i = 0; i < dataArray.Array.Count; i++)
        {
            checkerCorountines.Add(Waiter(i));
        }
        StartCoroutine(replacerCorountines[0]);

        sortingAlgorithm = null;
    }
    List<IEnumerator> replacerCorountines = new List<IEnumerator>();
    List<IEnumerator> checkerCorountines = new List<IEnumerator>();

    IEnumerator ReplaceElementsWaiter(int fromIndex, int toIndex, int i)
    {
        Debug.Log("Relocation operation");
        arrayVisualizer.MarkMainElemet(fromIndex);
        arrayVisualizer.MarkSecondaryElemet(toIndex);
        //yield return new WaitForSecondsRealtime(0.05f);
        yield return null;
        arrayVisualizer.SwitchElements(fromIndex, toIndex);
        if (i + 1 < replacerCorountines.Count)
        {
            Debug.Log("New relocation operation");
            StartCoroutine(replacerCorountines[i + 1]);
        }
        else
        {
            Debug.Log("Operations: " + i);
            StartCoroutine(checkerCorountines[0]);
        }
        Debug.LogWarning("Operations ended");
    }

    IEnumerator Waiter(int i) 
    {
        //yield return new WaitForSecondsRealtime(0.02f);
        yield return null;
        arrayVisualizer.MarkForCheck(i);
        if(i + 1 < checkerCorountines.Count)
            StartCoroutine(checkerCorountines[i + 1]);
        //checkerCorountines.Clear();
    }
}