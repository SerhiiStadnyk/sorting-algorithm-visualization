﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ColumnVisualizer : VisualizerBase
{
    private float minElementWidth;
    private float maxElementWidth;
    private int padding;
    private bool dynamicWidth;

    private List<Image> elementsList = new List<Image>();

    private float elementWidth;
    private ColumnVisualyzerSettings columnSettings;

    public ColumnVisualizer(ColumnVisualyzerSettings columnSettings, DataArray dataArray, RectTransform containerRect, Settings settings)
    {
        this.columnSettings = columnSettings;
        this.dataArray = dataArray;
        this.containerRect = containerRect;
        this.settings = settings;
        UpdateSettings();
    }

    private void UpdateSettings() 
    {
        minElementWidth = columnSettings.minElementWidth;
        maxElementWidth = columnSettings.maxElementWidth;
        padding = columnSettings.padding;
        dynamicWidth = columnSettings.dynamicWidth;

        switch (settings.SortingType)
        {
            default:
                visualizationColoring = new VisualizerColoringStandart(elementsList);
                break;
        }
    }

    public override void Build()
    {
        UpdateSettings();
        UpdateContainer();
    }

    public override void UpdateContainer()
    {
        if (dynamicWidth)
            maxElementWidth = CalculateDynamicWidth();
        elementWidth = GetElementWidth();

        int startingIndex = 0;

        if (elementsList.Count > 0)
        {
            for (int i = 0; i < elementsList.Count; i++)
            {
                if (dataArray.Array.Count > i)
                {
                    SetupElement(dataArray.Array[i], i);
                }
                else
                {
                    elementsList[i].gameObject.SetActive(false);
                }
                startingIndex++;
            }
        }

        for (int i = startingIndex; i < dataArray.Array.Count; i++)
        {
            CreateElement();
            SetupElement(dataArray.Array[i], i);
        }
    }

    public void SetupElement(int elementValue, int elementIndex)
    {
        Image element = elementsList[elementIndex];
        element.gameObject.SetActive(true);
        element.rectTransform.sizeDelta = new Vector2(elementWidth, GetElementHight(elementValue));
        element.transform.localPosition = GetElementPosition(elementIndex);
        element.color = Color.white;
    }

    public override void UpdateElement(int elementIndex)
    {
        Image element = elementsList[elementIndex];
        int elementValue = dataArray.Array[elementIndex];
        element.rectTransform.sizeDelta = new Vector2(elementWidth, GetElementHight(elementValue));
    }

    public void CreateElement()
    {
        Image image = new GameObject().AddComponent<Image>();
        image.rectTransform.pivot = new Vector2(0.5f, 0f);
        image.transform.SetParent(containerRect);
        image.transform.localScale = Vector3.one;
        elementsList.Add(image);
    }

    public float GetElementWidth()
    {
        float containerWidth = containerRect.rect.width;
        float dynamicWidth = containerWidth / dataArray.Array.Count;

        if (dynamicWidth > maxElementWidth)
            return maxElementWidth;
        else if (dynamicWidth > minElementWidth)
            return dynamicWidth;
        else
            return minElementWidth;
    }

    public float GetElementHight(int elementValue)
    {
        float result = 0;

        float maxHeight = containerRect.rect.height;
        float elementRatio = ((float)elementValue + 1f) / dataArray.Array.Count;

        result = maxHeight * elementRatio;

        return result;
    }

    public Vector2 GetElementPosition(int imageIndex)
    {
        Vector2 result = Vector2.zero;

        if ((maxElementWidth + padding) * dataArray.Array.Count < containerRect.rect.width)
        {
            result = GetElementPositionEqulibrium(imageIndex);
        }
        else
        {
            result = GetElementPositionFlat(imageIndex);
        }

        return result;
    }

    public Vector2 GetElementPositionFlat(int imageIndex)
    {
        float containerWidth = containerRect.rect.width;
        float startingX = containerWidth * 0.5f * -1;
        float xPos = startingX + (elementWidth) * imageIndex + (elementWidth * 0.5f);

        Vector2 result = new Vector2(xPos, containerRect.rect.height * 0.5f * -1);

        return result;
    }

    public Vector2 GetElementPositionEqulibrium(int imageIndex)
    {
        float containerWidth = containerRect.rect.width;
        float xOffset = 1f / (dataArray.Array.Count + 1);

        float gap = containerWidth * xOffset;

        float xPos = (containerWidth * 0.5f * -1) + gap * (imageIndex + 1);

        Vector2 result = new Vector2(xPos, containerRect.rect.height * 0.5f * -1);

        return result;
    }

    public float CalculateDynamicWidth()
    {
        float containerWidth = containerRect.rect.width;
        float result = containerWidth / 40;
        return result;
    }

    public override int CalculateMaxArrayNumber()
    {
        containerRect.ForceUpdateRectTransforms();
        float containerWidth = containerRect.rect.width;
        int maxElements = (int)((containerWidth) / (minElementWidth));
        Debug.Log("containerWidth: " + containerWidth);
        Debug.Log("maxElements: " + maxElements);
        float containerWidth_B = containerRect.sizeDelta.x;
        Debug.Log("containerWidth_B: " + containerWidth_B);

        return maxElements;
    }
}