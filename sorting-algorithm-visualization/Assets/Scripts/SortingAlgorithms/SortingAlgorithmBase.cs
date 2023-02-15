using System.Collections;
using System.Collections.Generic;
using ArrayVisualizer;

namespace SortingAlgorithms
{
    public abstract class SortingAlgorithmBase
    {
        public abstract IEnumerator Sort();


        protected SortingAlgorithmBase(ISortingHandable insertedRelocatable)
        {
            Handleable = insertedRelocatable;
        }


        protected virtual ISortingHandable Handleable { get; set; }

        protected List<int> Array { get => Handleable.Array; set => Handleable.Array = value; }
        protected virtual void CompareElements(bool count = false, params ElementColor[] markedElements) { Handleable.MarkElements(count, markedElements); }
        protected virtual void RelocateElements(int fromIndex, int toIndex) { Handleable.RelocateElements(fromIndex, toIndex); }
        protected virtual void FinishSorting() { Handleable.FinishSorting(); }
    }
}