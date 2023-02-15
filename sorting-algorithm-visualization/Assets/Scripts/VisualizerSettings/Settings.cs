using UnityEngine;

namespace VisualizerSettings
{
    public enum SortingTypes
    {
        Bubble,
        Shaker,
        Insertion,

        InsertionBinary

        //QuickSort
    }

    public enum RandomizerTypes
    {
        Sorted,
        Random,
        Inverse,
        Last,
        Half,
        HalfReverse,
        Mirrored,
        MirroredReverse,
        Pyramid,
        PyramidReverse
    }

    [CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/SpawnSettings", order = 1)]
    public class Settings : ScriptableObject
    {
        [SerializeField]
        private SortingTypes _sortingType;

        [SerializeField]
        private RandomizerTypes _randomizerType;

        [SerializeField]
        private int _minArraySize;

        public SortingTypes SortingType => _sortingType;

        public RandomizerTypes RandomizerType => _randomizerType;

        public int ArraySize { get; private set; }

        public int MaxArraySize { get; private set; }

        public int MinArraySize => _minArraySize;

        public int Delay { get; private set; }

        public int SortingTactsPerFrame { get; private set; } = 1;


        public void SetDelay(int value)
        {
            if (value < 0)
            {
                Delay = 0;
            }
            else
            {
                Delay = value;
            }
        }


        public void SetSortingTactsPerFrame(int value)
        {
            if (value <= 0)
            {
                SortingTactsPerFrame = 1;
            }
            else if (value <= ArraySize)
            {
                SortingTactsPerFrame = value;
            }
            else
            {
                SortingTactsPerFrame = ArraySize;
            }
        }


        public void SetSortingType(int id)
        {
            _sortingType = (SortingTypes)id;
        }


        public void SetRandomizerType(int id)
        {
            _randomizerType = (RandomizerTypes)id;
        }


        public void SetArraySize(int value)
        {
            if (value > MaxArraySize)
            {
                value = MaxArraySize;
            }
            else if (value < MinArraySize)
            {
                value = MinArraySize;
            }

            ArraySize = value;
        }


        public void SetMaximumArraySize(int value)
        {
            MaxArraySize = value;
            SetArraySize(ArraySize);
        }
    }
}