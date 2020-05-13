using System.Collections.Generic;

public class BubbleSorting : SortingAlgorithmBase
{
    public BubbleSorting(ISortingHandable handleable) : base(handleable) { }

    public override IEnumerable<int> Sort() 
    {
        for (int i = 0; i < Array.Count; i++)
        {
            bool isSorted = true;
            for (int a = 0; a < Array.Count - i - 1; a++)
            {
                if (Array[a] > Array[a + 1])
                {
                    int tmp = Array[a];
                    Array[a] = Array[a + 1];
                    Array[a + 1] = tmp;
                    RelocateElements(a, a + 1);
                    isSorted = false;
                }
                CompareElements(a, a + 1);
                yield return i;
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