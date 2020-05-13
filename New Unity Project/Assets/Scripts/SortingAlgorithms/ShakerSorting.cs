using System.Collections.Generic;
using UnityEngine;

public class ShakerSorting : SortingAlgorithmBase
{
    public override ISortingHandable Handleable { get; set; }

    public ShakerSorting(ISortingHandable handleable) : base(handleable) { }

    public override IEnumerable<int> Sort()
    {
        bool switcher = true;
        int left = 0;
        int rigth = 0;
        for (int i = 0; i < Array.Count; i++)
        {
            bool isSorted = true;
            if (switcher)
            {
                for (int a = rigth; a < Array.Count - rigth - 1; a++)
                {
                    if (Array[a] > Array[a + 1])
                    {
                        int tmp = Array[a];
                        Array[a] = Array[a + 1];
                        Array[a + 1] = tmp;
                        RelocateElements(a, a + 1);
                        isSorted = false;
                    }
                    switcher = false;
                    CompareElements(a, a + 1);
                    yield return i;
                }
                left++;
            }
            else 
            {
                for (int a = Array.Count - left - 1; a >= left; a--)
                {
                    if (Array[a] < Array[a - 1])
                    {
                        int tmp = Array[a];
                        Array[a] = Array[a - 1];
                        Array[a - 1] = tmp;
                        RelocateElements(a, a - 1);
                        isSorted = false;
                    }
                    switcher = true;
                    CompareElements(a, a - 1);
                    yield return i;
                }
                rigth++;
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