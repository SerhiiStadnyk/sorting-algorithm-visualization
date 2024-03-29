﻿using System.Collections;
using ArrayVisualizer;
using UnityEngine;

namespace SortingAlgorithms
{
    public class BubbleSorting : SortingAlgorithmBase
    {
        public BubbleSorting(ISortingHandable handleable) : base(handleable)
        {
        }


        public override IEnumerator Sort()
        {
            for (int i = 0; i < Array.Count; i++)
            {
                bool isSorted = true;
                for (int a = 0; a < Array.Count - i - 1; a++)
                {
                    if (Array[a] > Array[a + 1])
                    {
                        (Array[a], Array[a + 1]) = (Array[a + 1], Array[a]);
                        RelocateElements(a, a + 1);
                        isSorted = false;
                    }

                    CompareElements(
                        true,
                        ElementColor.Build(a, Color.red),
                        ElementColor.Build(a + 1, Color.red));

                    if (!Handleable.CanProceedSorting)
                    {
                        yield return new WaitUntil(() => Handleable.CanProceedSorting);
                    }
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
}