using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColumnVisualyzerSettings", menuName = "ScriptableObjects/SpawnColumnSettings", order = 1)]
public class ColumnVisualyzerSettings : ScriptableObject
{
    public float minElementWidth = 3;
    public float maxElementWidth = 25;
    public int padding = 5;
    public bool dynamicWidth = false;
}