using System.Collections.Generic;
using UnityEngine;

public class ShakerSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public ShakerSorting(ISortingHandable handleable) : base(handleable) { }

    private int leftCounter = 0;
    private int rightCounter = 0;
    bool wasSorted = false;

    public override IEnumerable<int> Sort()
    {
        bool flip = true;

        for (int i = 0; i < Array.Count - 1; i++)
        {
            if (flip) 
            {
                foreach (var a in LeftSort())
                    yield return 0;
                flip = false;
            }
            else
            {
                foreach (var a in RightSort())
                    yield return 0;
                flip = true;
            }

            if (!wasSorted)
                break;
        }

        FinishSorting();
    }

    int leftOffsetStart = 0;
    int leftOffsetEnd = 0;
    int rightOffsetStart = 0;
    int rightOffsetEnd = 0;
    private IEnumerable<int> LeftSort()
    {
        wasSorted = false;
        int offset = 0;
        int offsetA = 0;
        for (int i = 0 + rightCounter + leftOffsetStart; i < Array.Count - 1 - leftOffsetEnd - leftCounter; i++)
        {
            offsetA++;
            if (Array[i] > Array[i + 1]) 
            {
                offsetA = 0;
                wasSorted = true;
                int tmpVal = Array[i];
                Array[i] = Array[i + 1];
                Array[i + 1] = tmpVal;
                RelocateElements(i + 1, i);

            }
            if(!wasSorted)
                offset++;

            CompareElements(true,
            ElementColor.Build(i, Color.red),
            ElementColor.Build(i + 1, Color.red));
            yield return i;
        }

        leftCounter++;
        leftOffsetStart += offset - 1;
        leftOffsetEnd += offsetA;
    }
    private IEnumerable<int> RightSort()
    {
        wasSorted = false;
        int offset = 0;
        int offsetA = 0;
        for (int i = Array.Count - 1 - rightOffsetStart - leftCounter; i > 0 + rightOffsetEnd + rightCounter; i--)
        {
            offsetA++;
            if (Array[i] < Array[i - 1])
            {
                offsetA = 0;
                wasSorted = true;
                int tmpVal = Array[i];
                Array[i] = Array[i - 1];
                Array[i - 1] = tmpVal;
                RelocateElements(i - 1, i);
            }
            if (!wasSorted)
                offset++;

            CompareElements(true,
            ElementColor.Build(i, Color.red),
            ElementColor.Build(i - 1, Color.red));
            yield return i;
        }

        rightCounter++;
        rightOffsetStart += offset - 1;
        rightOffsetEnd += offsetA;
    }
}