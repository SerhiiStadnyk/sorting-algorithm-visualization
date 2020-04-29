using System.Collections;
using System.Collections.Generic;

public class BubbleSorting : SortingAlgorithmBase, ISortingHandable
{
    public override ISortingHandable Handleable { get; set; }
    public List<int> Array { get => Handleable.Array; set => Handleable.Array = value; }

    public BubbleSorting(ISortingHandable handleable) : base(handleable) { }

    private bool isSorted = true;

    public override void StartSorting()
    {

        for (int i = 0; i < Array.Count; i++)
        {
            isSorted = true;
            for (int a = 0; a < Array.Count - 1; a++)
            {
                if (Array[a] > Array[a + 1]) 
                {
                    int tmp = Array[a];
                    Array[a] = Array[a + 1];
                    Array[a + 1] = tmp;

                    RelocateElements(a, a + 1);
                }
            }
        }

        if (isSorted)
            FinishSorting();
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        isSorted = false;
        Handleable.RelocateElements(fromIndex, toIndex);
    }

    public void FinishSorting()
    {
        Handleable.FinishSorting();
    }
}