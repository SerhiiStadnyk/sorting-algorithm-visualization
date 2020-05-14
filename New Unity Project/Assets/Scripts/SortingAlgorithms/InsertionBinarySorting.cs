using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionBinarySorting : SortingAlgorithmBase
{
    private int binaryMark;
    int min;
    int max;

    public InsertionBinarySorting(ISortingHandable handleable) : base(handleable) { }
    public override IEnumerable<int> Sort()
    {
        for (int i = 1; i < Array.Count; i++)
        {
            if (Array[i] >= Array[i - 1])
                continue;

            if (Array[i] == 0) 
            {
                ShiftArray(0, i, Array);
            }

            binaryMark = i / 2;

            min = 0;
            max = i;

            while (min <= max)
            {
                int key = Array[i];
                int mid = (min + max) / 2;
                CompareElements(i, mid);
                yield return i;

                if (mid == Array.Count - 1) 
                {
                    ShiftArray(mid, i, Array);
                    //yield return i;
                    break;
                }

                Debug.Log(mid);
                if (key <= Array[mid + 1] && key >= Array[mid])
                {
                    ShiftArray(++mid, i, Array);
                    //yield return i;
                    break;
                }
                else if (key < Array[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }

                //CompareElements(i, binaryMark - 1, binaryMark + 1);
                //if (Array[i] <= Array[binaryMark + 1] && Array[i] >= Array[binaryMark - 1])
                //{
                //    ShiftArray(binaryMark, i, Array);
                //    yield return i;
                //    break;
                //}
                //else if (Array[i] < Array[binaryMark - 1] && Array[i] < Array[binaryMark + 1]) 
                //{
                //    binaryMark -= binaryMark / 2;
                //}
                //else if (Array[i] > Array[binaryMark - 1] && Array[i] > Array[binaryMark + 1])
                //{
                //    binaryMark += binaryMark / 2;
                //}

                yield return i;
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