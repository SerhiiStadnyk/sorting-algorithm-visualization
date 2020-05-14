using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualizersList", menuName = "ScriptableObjects/SpawnVisualizersList", order = 1)]
public class VisualizersList : ScriptableObject
{
    public ColumnVisualyzerSettings columnSettings;

    public VisualizerBase GetVisualizator(VisualizerTypes visualizerType, DataArray dataArray, RectTransform containerRect, Settings settings) 
    {
        switch (visualizerType)
        {
            case VisualizerTypes.Column:
                return GetVisualyzerColumn(dataArray, containerRect, settings);
            default:
                return GetVisualyzerColumn(dataArray, containerRect, settings);
        }
    }

    private VisualizerBase GetVisualyzerColumn(DataArray dataArray, RectTransform containerRect, Settings settings) 
    {
        ColumnVisualizer visualizer = new ColumnVisualizer(columnSettings, dataArray, containerRect, settings);
        return visualizer;
    }
}