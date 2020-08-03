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

    private IEnumerable<int> LeftSort()
    {
        wasSorted = false;
        for (int i = 0 + rightCounter; i < Array.Count - 1 - leftCounter; i++)
        {
            if (Array[i] > Array[i + 1]) 
            {
                wasSorted = true;
                int tmpVal = Array[i];
                Array[i] = Array[i + 1];
                Array[i + 1] = tmpVal;
                RelocateElements(i + 1, i);

            }
            CompareElements(true,
            ElementColor.Build(i, Color.red),
            ElementColor.Build(i + 1, Color.red));
            yield return i;
        }

        leftCounter++;
    }
    private IEnumerable<int> RightSort()
    {
        wasSorted = false;
        for (int i = Array.Count - 1 - leftCounter; i > 0 + rightCounter; i--)
        {
            if (Array[i] < Array[i - 1])
            {
                wasSorted = true;
                int tmpVal = Array[i];
                Array[i] = Array[i - 1];
                Array[i - 1] = tmpVal;
                RelocateElements(i - 1, i);
            }
            CompareElements(true,
            ElementColor.Build(i, Color.red),
            ElementColor.Build(i - 1, Color.red));
            yield return i;
        }

        rightCounter++;
    }
}