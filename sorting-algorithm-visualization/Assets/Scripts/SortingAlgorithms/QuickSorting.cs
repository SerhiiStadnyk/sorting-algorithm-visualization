using System.Collections.Generic;
using ArrayVisualizer;
using UnityEngine;

namespace SortingAlgorithms
{
    public class QuickSorting : SortingAlgorithmBase
    {
        public QuickSorting(ISortingHandable sortingHandable) : base(sortingHandable)
        {
        }


        public override IEnumerable<int> Sort()
        {
            int startPoint = 0;
            int endPoint = Array.Count - 1;
            int median = -1;
            var pivotList = new List<int>();
            int tmpEndPoint = 1;

            do
            {
                foreach (int i in SortChunk(startPoint, endPoint))
                {
                    yield return 0;
                }

                median = (endPoint - startPoint) / 2;
                if (endPoint > startPoint && median != 0)
                {
                    median = (endPoint - startPoint) / 2;
                    endPoint -= median;

                    if (endPoint != pivotList.Count - 1)
                    {
                        pivotList.Add(endPoint);
                    }

                    if (tmpEndPoint == 0)
                    {
                        tmpEndPoint = endPoint;
                    }
                }
                else
                {
                    //if (pivotList.Count >= 2)
                    //{
                    //    startPoint = pivotList[pivotList.Count - 1];
                    //    endPoint = pivotList[pivotList.Count - 2];
                    //    pivotList.RemoveAt(pivotList.Count - 1);
                    //}
                    //else if (pivotList.Count == 1)
                    //{
                    //    startPoint = pivotList[pivotList.Count - 1];
                    //    endPoint = Array.Count - 1;
                    //    pivotList.RemoveAt(pivotList.Count - 1);
                    //}
                    //else
                    //{
                    //    break;
                    //}

                    startPoint = endPoint + 1 + endPoint / 2;
                    endPoint = startPoint * 2;

                    tmpEndPoint = 0;

                    Debug.Log("=======================");
                    Debug.Log("Start Point " + startPoint);
                    Debug.Log("End Point " + endPoint);
                }

                yield return 0;
            }
            while (endPoint < Array.Count);

            FinishSorting();
        }


        private IEnumerable<int> SortChunk(int startIndex, int endIndex)
        {
            int median = GetMedianSimple(startIndex, endIndex);
            int rightOffset = 0;

            for (int i = startIndex; i < endIndex - rightOffset; i++)
            {
                CompareElements(
                    true,
                    ElementColor.Build(endIndex, Color.blue),
                    ElementColor.Build(startIndex, Color.blue),
                    ElementColor.Build(i, Color.green));

                if (Array[i] >= median)
                {
                    for (int a = endIndex - rightOffset; a > i; a--)
                    {
                        CompareElements(
                            true,
                            ElementColor.Build(i, Color.green),
                            ElementColor.Build(a, Color.red),
                            ElementColor.Build(endIndex, Color.blue),
                            ElementColor.Build(startIndex, Color.blue));

                        rightOffset++;
                        if (Array[a] < median)
                        {
                            SwapElements(i, a);

                            //yield return a;
                            break;
                        }

                        yield return a;
                    }
                }

                yield return i;
            }
        }


        private void SwapElements(int fromIndex, int toIndex)
        {
            (Array[fromIndex], Array[toIndex]) = (Array[toIndex], Array[fromIndex]);

            RelocateElements(fromIndex, toIndex);
        }


        private int GetMedian(int arrayCount)
        {
            var medianList = new List<int>();

            if (arrayCount >= 9)
            {
                for (int i = 0; i < arrayCount; i++)
                {
                    medianList.Add(i);

                    if (i >= 2 && i < arrayCount / 2 - 1)
                    {
                        i = arrayCount / 2 - 1;
                    }
                    else if (i >= arrayCount / 2 + 1 && i < arrayCount - 4)
                    {
                        i = arrayCount - 4;
                    }
                }
            }

            int sum = 0;
            medianList.ForEach(value => sum += value);
            if (medianList.Count > 0)
            {
                sum = sum / medianList.Count;
            }

            return sum;
        }


        private int GetMedianSimple(int startIndex, int endIndex)
        {
            return endIndex - (endIndex - startIndex) / 2;
        }
    }
}