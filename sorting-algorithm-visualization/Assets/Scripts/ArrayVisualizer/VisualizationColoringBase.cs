using System.Collections.Generic;
using UnityEngine.UI;

namespace ArrayVisualizer
{
    public abstract class VisualizationColoringBase
    {
        protected readonly List<Image> elementsList;
        protected readonly List<Image> markedElements = new List<Image>();
        protected readonly List<ElementColor> marksList = new List<ElementColor>();


        public virtual void RemoveMarks()
        {
        }


        public virtual void MarkElements()
        {
        }


        public virtual void AddMarks(params ElementColor[] indexArray)
        {
        }


        public virtual void MarkForCheck(int index, bool isWrong)
        {
        }


        protected VisualizationColoringBase(List<Image> elementsList)
        {
            this.elementsList = elementsList;
        }
    }
}