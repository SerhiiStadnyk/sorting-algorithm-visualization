﻿using System.Collections.Generic;
using UnityEngine;

public class InsertionSorting : SortingAlgorithmBase
{
    public InsertionSorting(ISortingHandable handleable) : base(handleable) { }
    public override IEnumerable<int> Sort()
    {
        for (int i = 1; i < Array.Count; i++)
        {
            if (Array[i] >= Array[i - 1])
            {
                continue;
            }
            CompareElements(true,
                ElementColor.Build(i, Color.green),
                ElementColor.Build(i - 1, Color.red));
            yield return i;

            for (int a = i; a > 0; a--)
            {
                int b = a - 1;
                CompareElements(true,
                    ElementColor.Build(i, Color.green),
                    ElementColor.Build(b, Color.red));
                yield return a;
                if (Array[i] >= Array[b])
                {
                    ShiftArray(b + 1, i, Array);
                    break;
                }
                else if (b == 0)
                {
                    ShiftArray(b, i, Array);
                    break;
                }

            }
        }

        FinishSorting();
    }

    private void ShiftArray(int fromIndex, int toIndex, List<int> array) 
    {
        int frontElement = array[toIndex];
        for (int i = toIndex; i > fromIndex; i--)
        {
            array[i] = array[i - 1];
            RelocateElements(i, i - 1);
        }
        array[fromIndex] = frontElement;
        RelocateElements(fromIndex, 0);
    }
}