using System.Collections.Generic;
using UnityEngine;

public class ShakerSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public ShakerSorting(ISortingHandable handleable) : base(handleable) { }

    public override IEnumerable<int> Sort()
    {
        bool switcher = true;
        int leftEnd = 0;
        int leftStart = 0;
        int rigthEnd = 0;
        int rigthStart = 0;
        int leftOffset = 0;
        int leftOffsetStart = 0;
        int rigthOffset = 0;
        int rigthOffsetStart = 0;

        int tmpOffset = 0;
        int tmpOffsetStart = 0;

        for (int i = 0; i < Array.Count;)
        {
            tmpOffset = 0;
            tmpOffsetStart = 0;

            bool isSorted = true;
            if (switcher)
            {
                leftEnd = Array.Count - i - 1 - leftOffset;
                leftStart = leftOffsetStart;
                for (int a = leftStart; a < leftEnd; a++)
                {
                    tmpOffset++;
                    if (Array[a] > Array[a + 1])
                    {
                        int tmp = Array[a];
                        Array[a] = Array[a + 1];
                        Array[a + 1] = tmp;
                        RelocateElements(a, a + 1);
                        tmpOffset = 0;
                        isSorted = false;
                    }
                    if (isSorted)
                        tmpOffsetStart++;
                    CompareElements(a, a + 1);
                    yield return i;
                }
                leftOffset += tmpOffset;
                leftOffsetStart += tmpOffsetStart;
                switcher = false;
                i++;
            }
            else 
            {
                rigthEnd = i + rigthOffset;
                rigthStart = Array.Count - 1 - rigthOffsetStart;
                for (int a = rigthStart; a >= rigthEnd; a--)
                {
                    tmpOffset++;
                    if (Array[a] < Array[a - 1])
                    {
                        int tmp = Array[a];
                        Array[a] = Array[a - 1];
                        Array[a - 1] = tmp;
                        RelocateElements(a, a - 1);
                        tmpOffset = 0;
                        isSorted = false;
                    }
                    if (isSorted)
                        tmpOffsetStart++;
                    CompareElements(a, a - 1);
                    yield return i;
                }
                rigthOffset += tmpOffset;
                rigthOffsetStart += tmpOffsetStart;
                switcher = true;
            }
            if (isSorted)
            {
                FinishSorting();
                yield break;
            }
        }
        FinishSorting();
    }
}