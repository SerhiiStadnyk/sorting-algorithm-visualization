﻿using System.Collections;
using System.Collections.Generic;

public abstract class SortingAlgorithmBase
{
    public abstract ISortingHandable Handleable { get; set; }

    public abstract bool IsSorted { get; }

    public abstract void StartSorting();

    public abstract void SortingStep();

    public SortingAlgorithmBase(ISortingHandable insertedRelocatable)
    {
        Handleable = insertedRelocatable;
    }
}