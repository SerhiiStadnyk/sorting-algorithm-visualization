using System.Collections.Generic;
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


        public override IEnumerable<int> Sort()
        {
            bool flip = true;

            for (int i = 0; i < Array.Count - 1; i++)
            {
                if (flip)
                {
                    foreach (int a in LeftSort())
                    {
                        yield return 0;
                    }

                    flip = false;
                }
                else
                {
                    foreach (int a in RightSort())
                    {
                        yield return 0;
                    }

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


        private IEnumerable<int> LeftSort()
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
                yield return i;
            }

            _leftCounter++;
            _leftOffsetStart += offset - 1;
            _leftOffsetEnd += offsetA;
        }


        private IEnumerable<int> RightSort()
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
                yield return i;
            }

            _rightCounter++;
            _rightOffsetStart += offset - 1;
            _rightOffsetEnd += offsetA;
        }
    }
}