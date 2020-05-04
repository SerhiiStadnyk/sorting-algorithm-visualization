using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSorting : SortingAlgorithmBase, ISortingHandable
{
    public override ISortingHandable Handleable { get; set; }
    public List<int> Array { get => Handleable.Array; set => Handleable.Array = value; }

    public BubbleSorting(ISortingHandable handleable) : base(handleable) { }

    private bool arrayChanged = false;
    private bool isSorted = false;
    public override bool IsSorted => isSorted;

    private int stepA = 0;
    private int stepB = 0;

    public override void StartSorting()
    {
        Sorting();
    }

    private void Sorting() 
    {
        for (int i = 0; i < Array.Count; i++)
        {
            isSorted = true;
            for (int a = 0; a < Array.Count - 1 - i; a++)
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

        if (IsSorted)
            FinishSorting();
    }

    public override void SortingStep()
    {
        if (stepB == 0)
            arrayChanged = false;

        if (stepA < Array.Count)
        {
            if (stepB < Array.Count - stepA - 1)
            {

                if (Array[stepB] > Array[stepB + 1])
                {
                    int tmp = Array[stepB];
                    Array[stepB] = Array[stepB + 1];
                    Array[stepB + 1] = tmp;

                    RelocateElements(stepB, stepB + 1);
                }

                MarkElements(stepB, stepB + 1);

                stepB++;
                return;
            }

            if (arrayChanged == false)
            {
                isSorted = true;
                return;
            }

            stepB = 0;
            stepA++;
            return;
        }

        isSorted = true;
    }

    public void MarkElements(params int[] markedElements) 
    {
        Handleable.MarkElements(markedElements);
    }

    public void RelocateElements(int fromIndex, int toIndex)
    {
        arrayChanged = true;
        Handleable.RelocateElements(fromIndex, toIndex);
    }

    public void FinishSorting()
    {
        Handleable.FinishSorting();
    }
}