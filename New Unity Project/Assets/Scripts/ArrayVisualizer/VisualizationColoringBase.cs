using System.Collections.Generic;
using UnityEngine.UI;

public abstract class VisualizationColoringBase
{
    protected List<Image> elementsList = new List<Image>();
    protected List<Image> markedElements = new List<Image>();
    protected List<ElementColor> marksList = new List<ElementColor>();

    public VisualizationColoringBase(List<Image> elementsList)
    {
        this.elementsList = elementsList;
    }

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
}