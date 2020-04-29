using System.Collections;
using System.Collections.Generic;

public abstract class SortingAlgorithmBase
{
    public abstract ISortingHandable Handleable { get; set; }

    public abstract void StartSorting();

    public SortingAlgorithmBase(ISortingHandable insertedRelocatable)
    {
        Handleable = insertedRelocatable;
    }
}