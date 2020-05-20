using UnityEngine;

public enum VisualizerTypes 
{
    Column
}
abstract public class VisualizerBase
{
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

    protected RectTransform containerRect;
    protected DataArray dataArray;
    protected VisualizationColoringBase visualizationColoring;
    protected Settings settings;
}