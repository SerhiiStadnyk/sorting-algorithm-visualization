using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSorting : SortingAlgorithmBase
{
    public InsertionSorting(ISortingHandable handleable) : base(handleable) { }
    public override IEnumerable<int> Sort()
    {
        yield return 1;
    }
}