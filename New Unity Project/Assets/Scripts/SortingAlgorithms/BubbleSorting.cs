using System.Collections;
using System.Collections.Generic;

public class BubbleSorting : SortingAlgorithmBase
{
    public override ISortingHandleable Handleable { get; set; }

    public BubbleSorting(ISortingHandleable handleable) : base(handleable) { }

    public override void StartSorting()
    {
    }
}