using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISortingHandleable
{
    void RelocateElements(int fromIndex, int toIndex);
    void FinishSorting();
}