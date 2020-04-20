using UnityEngine;

public class SortingController : MonoBehaviour, ISortingHandleable
{
    private SortingAlgorithmBase sortingAlgorithm;
    //ArrayVisualizer arrayVisualizer;

    [SerializeField] private DataArray dataArray = null;
    [SerializeField] private Settings settings = null;

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