using UnityEngine;

public enum VisualizerTypes 
{
    Column
}
abstract public class VisualizerBase
{
    public abstract int CalculateMaxArrayNumber();
    public abstract void Build();
    public abstract void MarkForCheck(int index, bool isWrong);
    public abstract void AddMarks(params int[] indexArray);
    public abstract void MarkElements();
    public abstract void RemoveMarks();
    public abstract void UpdateElement(int elementIndex);
    public abstract void UpdateContainer();

    protected RectTransform containerRect;
    protected DataArray dataArray;
}