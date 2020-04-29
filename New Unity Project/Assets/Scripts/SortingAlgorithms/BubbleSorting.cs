using System.Collections;
using System.Collections.Generic;

public class BubbleSorting : SortingAlgorithmBase, ISortingHandleable
{
    public override ISortingHandleable Handleable { get; set; }

    public BubbleSorting(ISortingHandleable handleable) : base(handleable) { }

    public override void StartSorting()
    {
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        Handleable.RelocateElements(fromIndex, toIndex);
    }

    public void FinishSorting()
    {
        Handleable.FinishSorting();
    }
}