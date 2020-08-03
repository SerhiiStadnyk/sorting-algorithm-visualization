using System.Collections.Generic;

public abstract class SortingAlgorithmBase
{
    public virtual ISortingHandable Handleable { get; set; }
    public virtual void CompareElements(bool count = false, params ElementColor[] markedElements) { Handleable.MarkElements(count, markedElements); }
    public virtual void RelocateElements(int fromIndex, int toIndex) { Handleable.RelocateElements(fromIndex, toIndex); }
    public virtual void FinishSorting() { Handleable.FinishSorting(); }
    public abstract IEnumerable<int> Sort();

    public List<int> Array { get => Handleable.Array; set => Handleable.Array = value; }

    public SortingAlgorithmBase(ISortingHandable insertedRelocatable)
    {
        Handleable = insertedRelocatable;
    }
}