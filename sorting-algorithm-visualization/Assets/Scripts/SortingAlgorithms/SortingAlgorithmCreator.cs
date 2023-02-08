using VisualizerSettings;

namespace SortingAlgorithms
{
    /// <summary>
    ///     Factory pattern for sorting algoritms
    /// </summary>
    public static class SortingAlgorithmCreator
    {
        public static SortingAlgorithmBase GetAlgorithm(ISortingHandable handleable, SortingTypes sortingType)
        {
            SortingAlgorithmBase result = null;

            switch (sortingType)
            {
                case SortingTypes.Bubble:
                    result = CreateBubbleSorting(handleable);
                    break;
                case SortingTypes.Shaker:
                    result = CreateShakerSorting(handleable);
                    break;
                case SortingTypes.Insertion:
                    result = CreateInsertionSorting(handleable);
                    break;
                case SortingTypes.InsertionBinary:
                    result = CreateInsertionBinarySorting(handleable);
                    break;

                //case SortingTypes.QuickSort:
                //    result = SortingAlgorithmCreator.CreateQuickSort(handleable);
                //    break;
            }

            return result;
        }


        private static SortingAlgorithmBase CreateBubbleSorting(ISortingHandable handleable)
        {
            return new BubbleSorting(handleable);
        }


        private static SortingAlgorithmBase CreateShakerSorting(ISortingHandable handleable)
        {
            return new ShakerSorting(handleable);
        }


        private static SortingAlgorithmBase CreateInsertionSorting(ISortingHandable handleable)
        {
            return new InsertionSorting(handleable);
        }


        private static SortingAlgorithmBase CreateInsertionBinarySorting(ISortingHandable handleable)
        {
            return new InsertionBinarySorting(handleable);
        }


        private static SortingAlgorithmBase CreateQuickSort(ISortingHandable handleable)
        {
            return new QuickSorting(handleable);
        }
    }
}