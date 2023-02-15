using System.Collections;
using ArrayVisualizer;
using UnityEngine;

namespace SortingAlgorithms
{
    public class ShakerSorting : SortingAlgorithmBase
    {
        private int _leftCounter;
        private int _leftOffsetEnd;

        private int _leftOffsetStart;
        private int _rightCounter;
        private int _rightOffsetEnd;
        private int _rightOffsetStart;
        private bool _wasSorted;


        public ShakerSorting(ISortingHandable handleable) : base(handleable)
        {
        }


        public override IEnumerator Sort()
        {
            bool flip = true;

            for (int i = 0; i < Array.Count - 1; i++)
            {
                if (flip)
                {
                    yield return LeftSort();

                    flip = false;
                }
                else
                {
                    yield return RightSort();

                    flip = true;
                }

                if (!_wasSorted)
                {
                    break;
                }
            }

            FinishSorting();
        }


        protected override ISortingHandable Handleable { get; set; }


        private IEnumerator LeftSort()
        {
            _wasSorted = false;
            int offset = 0;
            int offsetA = 0;
            for (int i = 0 + _rightCounter + _leftOffsetStart; i < Array.Count - 1 - _leftOffsetEnd - _leftCounter; i++)
            {
                offsetA++;
                if (Array[i] > Array[i + 1])
                {
                    offsetA = 0;
                    _wasSorted = true;
                    (Array[i], Array[i + 1]) = (Array[i + 1], Array[i]);
                    RelocateElements(i + 1, i);
                }

                if (!_wasSorted)
                {
                    offset++;
                }

                CompareElements(
                    true,
                    ElementColor.Build(i, Color.red),
                    ElementColor.Build(i + 1, Color.red));
                if (!Handleable.CanProceedSorting)
                {
                    yield return new WaitUntil(() => Handleable.CanProceedSorting);
                }
            }

            _leftCounter++;
            _leftOffsetStart += offset - 1;
            _leftOffsetEnd += offsetA;
        }


        private IEnumerator RightSort()
        {
            _wasSorted = false;
            int offset = 0;
            int offsetA = 0;
            for (int i = Array.Count - 1 - _rightOffsetStart - _leftCounter; i > 0 + _rightOffsetEnd + _rightCounter; i--)
            {
                offsetA++;
                if (Array[i] < Array[i - 1])
                {
                    offsetA = 0;
                    _wasSorted = true;
                    (Array[i], Array[i - 1]) = (Array[i - 1], Array[i]);
                    RelocateElements(i - 1, i);
                }

                if (!_wasSorted)
                {
                    offset++;
                }

                CompareElements(
                    true,
                    ElementColor.Build(i, Color.red),
                    ElementColor.Build(i - 1, Color.red));
                if (!Handleable.CanProceedSorting)
                {
                    yield return new WaitUntil(() => Handleable.CanProceedSorting);
                }
            }

            _rightCounter++;
            _rightOffsetStart += offset - 1;
            _rightOffsetEnd += offsetA;
        }
    }
}