using System.Collections.Generic;
using ArrayVisualizer;

public interface ISortingHandable
{
    List<int> Array { get; set; }

    bool CanProceedSorting { get; }

    void RelocateElements(int fromIndex, int toIndex);
    void MarkElements(bool count = false, params ElementColor[] markedElements);
    void FinishSorting();
}