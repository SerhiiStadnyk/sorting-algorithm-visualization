using System.Collections;
using System.Collections.Generic;
using ArrayVisualizer;
using UnityEngine;

namespace SortingAlgorithms
{
    public class InsertionBinarySorting : SortingAlgorithmBase
    {
        private int _max;
        private int _min;


        public InsertionBinarySorting(ISortingHandable handleable) : base(handleable)
        {
        }


        public override IEnumerator Sort()
        {
            for (int i = 1; i < Array.Count; i++)
            {
                if (Array[i] >= Array[i - 1])
                {
                    continue;
                }

                CompareElements(true, ElementColor.Build(i, Color.green));
                if (!Handleable.CanProceedSorting)
                {
                    yield return new WaitUntil(() => Handleable.CanProceedSorting);
                }

                _min = 0;
                _max = i;

                while (_min <= _max)
                {
                    int key = Array[i];
                    int mid = (_min + _max) / 2;
                    CompareElements(
                        true,
                        ElementColor.Build(i, Color.green),
                        ElementColor.Build(mid, Color.red));
                    if (!Handleable.CanProceedSorting)
                    {
                        yield return new WaitUntil(() => Handleable.CanProceedSorting);
                    }

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

                    if (key < Array[mid])
                    {
                        _max = mid - 1;
                    }
                    else
                    {
                        _min = mid + 1;
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
}