using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayVisualizerController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private VisualizersList visualizersList;
    [SerializeField] private Settings settings;
#pragma warning restore 0649

    private VisualizerBase visualizerBase;

    public void Init(VisualizerTypes visualizerType, DataArray dataArray) 
    {
        visualizerBase = visualizersList.GetVisualizator(visualizerType, dataArray, containerRect, settings);
    }

    public void Build() 
    {
        visualizerBase.Build();
    }

    public void UpdateElement(int elementIndex) 
    {
        visualizerBase.UpdateElement(elementIndex);
    }

    public void RemoveMarks() 
    {
        visualizerBase.RemoveMarks();
    }
    public void MarkElements() 
    {
        visualizerBase.MarkElements();
    }
    public void AddMarks(params int[] indexArray) 
    {
        visualizerBase.AddMarks(indexArray);
    }

    public void MarkForCheck(int index, bool isWrong) 
    {
        visualizerBase.MarkForCheck(index, isWrong);
    }

    public int CalculateMaxArraySize() 
    {
        return visualizerBase.CalculateMaxArrayNumber();
    }
}