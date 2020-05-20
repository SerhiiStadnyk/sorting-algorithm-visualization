using System.Collections.Generic;
using UnityEngine;

public class ShakerSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public ShakerSorting(ISortingHandable handleable) : base(handleable) { }

    private bool switcher = true;
    private int leftEnd = 0;
    private int leftStart = 0;
    private int rigthEnd = 0;
    private int rigthStart = 0;
    private int leftOffset = 0;
    private int leftOffsetStart = 0;
    private int rigthOffset = 0;
    private int rigthOffsetStart = 0;

    private int tmpOffset = 0;
    private int tmpOffsetStart = 0;

    bool isSorted = true;

    public override IEnumerable<int> Sort()
    {
        for (int i = 0; i < Array.Count;)
        {
            tmpOffset = 0;
            tmpOffsetStart = 0;
            isSorted = true;

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
                    CompareElements(
                        ElementColor.Build(a, Color.red),
                        ElementColor.Build(a+1, Color.red));
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
                    CompareElements(
                        ElementColor.Build(a, Color.red),
                        ElementColor.Build(a - 1, Color.red));
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