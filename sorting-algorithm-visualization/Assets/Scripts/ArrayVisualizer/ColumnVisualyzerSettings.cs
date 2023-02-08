using UnityEngine;
using UnityEngine.Serialization;

namespace ArrayVisualizer
{
    [CreateAssetMenu(fileName = "ColumnVisualyzerSettings", menuName = "ScriptableObjects/SpawnColumnSettings", order = 1)]
    public class ColumnVisualyzerSettings : ScriptableObject
    {
        [FormerlySerializedAs("minElementWidth")]
        [SerializeField]
        private float _minElementWidth = 3;

        [FormerlySerializedAs("maxElementWidth")]
        [SerializeField]
        private float _maxElementWidth = 25;

        [FormerlySerializedAs("padding")]
        [SerializeField]
        private int _padding = 5;

        [FormerlySerializedAs("dynamicWidth")]
        [SerializeField]
        private bool _dynamicWidth;

        public float MinElementWidth => _minElementWidth;

        public float MaxElementWidth => _maxElementWidth;

        public int Padding => _padding;

        public bool DynamicWidth => _dynamicWidth;
    }
}