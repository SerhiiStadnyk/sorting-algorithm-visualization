using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualizersList", menuName = "ScriptableObjects/SpawnVisualizersList", order = 1)]
public class VisualizersList : ScriptableObject
{
    public ColumnVisualyzerSettings columnSettings;

    public VisualizerBase GetVisualizator(VisualizerTypes visualizerType, DataArray dataArray, RectTransform containerRect) 
    {
        switch (visualizerType)
        {
            case VisualizerTypes.Column:
                return GetVisualyzerColumn(dataArray, containerRect);
            default:
                return GetVisualyzerColumn(dataArray, containerRect);
        }
    }

    private VisualizerBase GetVisualyzerColumn(DataArray dataArray, RectTransform containerRect) 
    {
        ColumnVisualizer visualizer = new ColumnVisualizer(columnSettings, dataArray, containerRect);
        return visualizer;
    }
}