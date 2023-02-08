using UnityEngine;
using UnityEngine.Serialization;
using VisualizerSettings;

namespace ArrayVisualizer
{
    public class ArrayVisualizerController : MonoBehaviour
    {
        [FormerlySerializedAs("containerRect")]
        [SerializeField]
        private RectTransform _containerRect;

        [FormerlySerializedAs("visualizersList")]
        [SerializeField]
        private VisualizersList _visualizersList;

        [FormerlySerializedAs("settings")]
        [SerializeField]
        private Settings _settings;

        private VisualizerBase _visualizerBase;


        public void Init(VisualizerTypes visualizerType, DataArray dataArray)
        {
            _visualizerBase = _visualizersList.GetVisualizer(visualizerType, dataArray, _containerRect, _settings);
        }


        public void Build()
        {
            _visualizerBase.Build();
        }


        public void UpdateElement(int elementIndex)
        {
            _visualizerBase.UpdateElement(elementIndex);
        }


        public void RemoveMarks()
        {
            _visualizerBase.RemoveMarks();
        }


        public void MarkElements()
        {
            _visualizerBase.MarkElements();
        }


        public void AddMarks(params ElementColor[] indexArray)
        {
            _visualizerBase.AddMarks(indexArray);
        }


        public void MarkForCheck(int index, bool isWrong)
        {
            _visualizerBase.MarkForCheck(index, isWrong);
        }


        public int CalculateMaxArraySize()
        {
            return _visualizerBase.CalculateMaxArrayNumber();
        }
    }
}