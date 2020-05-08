using System.Collections.Generic;

public class ShakerSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public ShakerSorting(ISortingHandable handleable) : base(handleable) { }


    public void MarkElements(params int[] markedElements)
    {
        Handleable.MarkElements(markedElements);
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        Handleable.RelocateElements(fromIndex, toIndex);
    }

    public void FinishSorting()
    {
        Handleable.FinishSorting();
    }

    public override IEnumerable<int> Sort()
    {
        throw new System.NotImplementedException();
    }
}