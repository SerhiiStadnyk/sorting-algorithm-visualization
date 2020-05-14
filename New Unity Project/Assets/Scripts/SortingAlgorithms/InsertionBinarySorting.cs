using System.Collections.Generic;

public class InsertionBinarySorting : SortingAlgorithmBase
{
    int min;
    int max;

    public InsertionBinarySorting(ISortingHandable handleable) : base(handleable) { }
    public override IEnumerable<int> Sort()
    {
        for (int i = 1; i < Array.Count; i++)
        {
            if (Array[i] >= Array[i - 1])
            {
                CompareElements(i);
                yield return i;
                continue;
            }

            min = 0;
            max = i;

            while (min <= max)
            {
                int key = Array[i];
                int mid = (min + max) / 2;
                CompareElements(i, mid);
                yield return i;

                if (mid == 0 && key <= Array[mid])
                {
                    ShiftArray(mid, i, Array);
                    break;
                }
                if (key <= Array[mid + 1] && key >= Array[mid])
                {
                    ShiftArray(++mid, i, Array);
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