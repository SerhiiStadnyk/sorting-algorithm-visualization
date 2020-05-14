using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSorting : SortingAlgorithmBase
{
    public InsertionSorting(ISortingHandable handleable) : base(handleable) { }
    public override IEnumerable<int> Sort()
    {
        for (int i = 1; i < Array.Count; i++)
        {
            CompareElements(i, i - 1);
            if (Array[i] >= Array[i - 1])
                continue;

            for (int a = i; a > 0; a--)
            {
                int b = a - 1;
                CompareElements(i, b);
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

                yield return a;
            }
            yield return i;
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