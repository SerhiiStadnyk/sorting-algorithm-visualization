using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizerColoringStandart: VisualizationColoringBase
{

    public VisualizerColoringStandart(List<Image> elementsList): base(elementsList)
    {
    }

    public override void RemoveMarks()
    {
        elementsList.ForEach(image => image.color = Color.white);
    }
    public override void MarkElements()
    {
        if (markedElements.Count > 0)
        {
            markedElements.ForEach(image => image.color = Color.white);
            markedElements.Clear();
        }

        for (int i = 0; i < marksList.Count; i++)
        {
            markedElements.Add(elementsList[marksList[i]]);
            elementsList[marksList[i]].color = Color.red;

            if (i == 0)
                elementsList[marksList[i]].color = Color.red;
        }

        marksList.Clear();
    }

    public override void AddMarks(params int[] indexArray)
    {
        for (int i = 0; i < indexArray.Length; i++)
        {
            marksList.Add(indexArray[i]);
        }
    }

    public override void MarkForCheck(int index, bool isWrong)
    {
        if (isWrong)
            elementsList[index].color = Color.red;
        else
            elementsList[index].color = Color.green;
    }
}
