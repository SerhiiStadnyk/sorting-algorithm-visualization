using System.Collections;
using System.Collections.Generic;

public abstract class SortingAlgorithmBase
{
    public abstract ISortingHandleable Handleable { get; set; }

    public abstract void StartSorting();

    public SortingAlgorithmBase(ISortingHandleable insertedRelocatable)
    {
        Handleable = insertedRelocatable;
    }
}