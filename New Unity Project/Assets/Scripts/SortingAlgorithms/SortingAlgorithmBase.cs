using System.Collections.Generic;

public abstract class SortingAlgorithmBase
{
    public abstract ISortingHandable Handleable { get; set; }

    public abstract void SortingStep();

    public List<int> Array { get => Handleable.Array; set => Handleable.Array = value; }

    public SortingAlgorithmBase(ISortingHandable insertedRelocatable)
    {
        Handleable = insertedRelocatable;
    }
}