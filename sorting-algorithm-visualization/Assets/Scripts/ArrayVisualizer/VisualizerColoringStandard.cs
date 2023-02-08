using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArrayVisualizer
{
    public class VisualizerColoringStandard : VisualizationColoringBase
    {
        public VisualizerColoringStandard(List<Image> elementsList) : base(elementsList)
        {
        }


        public override void RemoveMarks()
        {
            elementsList.ForEach(image => image.color = Color.white);
        }


        public override void MarkElements()
        {
            if (marksList.Count == 0)
            {
                return;
            }

            if (markedElements.Count > 0)
            {
                markedElements.ForEach(image => image.color = Color.white);
                markedElements.Clear();
            }

            for (int i = 0; i < marksList.Count; i++)
            {
                markedElements.Add(elementsList[marksList[i].ElementIndex]);
                elementsList[marksList[i].ElementIndex].color = marksList[i].ElementColor1;

                if (i == 0)
                {
                    elementsList[marksList[i].ElementIndex].color = marksList[i].ElementColor1;
                }
            }

            marksList.Clear();
        }


        public override void AddMarks(params ElementColor[] indexArray)
        {
            for (int i = 0; i < indexArray.Length; i++)
            {
                marksList.Add(indexArray[i]);
            }
        }


        public override void MarkForCheck(int index, bool isWrong)
        {
            if (isWrong)
            {
                elementsList[index].color = Color.red;
            }
            else
            {
                elementsList[index].color = Color.green;
            }
        }
    }
}