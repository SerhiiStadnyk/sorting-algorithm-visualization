using UnityEngine;
using VisualizerSettings;

namespace ArrayVisualizer
{
    public enum VisualizerTypes
    {
        Column
    }

    public abstract class VisualizerBase
    {
        protected RectTransform containerRect;
        protected DataArray dataArray;
        protected Settings settings;
        protected VisualizationColoringBase visualizationColoring;

        public abstract int CalculateMaxArrayNumber();
        public abstract void Build();
        public abstract void UpdateElement(int elementIndex);
        public abstract void UpdateContainer();


        public virtual void RemoveMarks()
        {
            visualizationColoring.RemoveMarks();
        }


        public virtual void MarkElements()
        {
            visualizationColoring.MarkElements();
        }


        public virtual void AddMarks(params ElementColor[] indexArray)
        {
            visualizationColoring.AddMarks(indexArray);
        }


        public virtual void MarkForCheck(int index, bool isWrong)
        {
            visualizationColoring.MarkForCheck(index, isWrong);
        }
    }
}