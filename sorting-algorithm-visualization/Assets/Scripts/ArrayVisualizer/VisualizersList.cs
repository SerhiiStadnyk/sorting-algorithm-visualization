using UnityEngine;
using UnityEngine.Serialization;
using VisualizerSettings;

namespace ArrayVisualizer
{
    [CreateAssetMenu(fileName = "VisualizersList", menuName = "ScriptableObjects/SpawnVisualizersList", order = 1)]
    public class VisualizersList : ScriptableObject
    {
        [FormerlySerializedAs("columnSettings")]
        [SerializeField]
        private ColumnVisualyzerSettings _columnSettings;


        public VisualizerBase GetVisualizer(
            VisualizerTypes visualizerType,
            DataArray dataArray,
            RectTransform containerRect,
            Settings settings)
        {
            switch (visualizerType)
            {
                case VisualizerTypes.Column:
                    return GetVisualizerColumn(dataArray, containerRect, settings);
                default:
                    return GetVisualizerColumn(dataArray, containerRect, settings);
            }
        }


        private VisualizerBase GetVisualizerColumn(DataArray dataArray, RectTransform containerRect, Settings settings)
        {
            ColumnVisualizer visualizer = new ColumnVisualizer(_columnSettings, dataArray, containerRect, settings);
            return visualizer;
        }
    }
}