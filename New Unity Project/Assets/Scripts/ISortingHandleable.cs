using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISortingHandable
{
    List<int> Array { get; set; }
    void RelocateElements(int fromIndex, int toIndex);
    void MarkElements(params int[] markedElements);
    void FinishSorting();
}