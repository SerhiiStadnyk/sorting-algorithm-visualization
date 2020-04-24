using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayVisualizerController : MonoBehaviour
{
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private HorizontalLayoutGroup horizontalLayout;

    [SerializeField] private GameObject imagePrefab;

    [SerializeField] private float minElementWidth = 3;
    [SerializeField] private float maxElementWidth = 25;
    [SerializeField] private int padding = 5;

    [SerializeField] private bool dinamicWidth = false;

    private Settings settings;
    private DataArray dataArray;
    private List<Image> elementsList = new List<Image>();

    private float elementWidth;

    public void Init(Settings settings, DataArray dataArray) 
    {
        this.settings = settings;
        this.dataArray = dataArray;
        settings.SetMaximumArraySize(CalculateMaxArrayNumber());
        if(dinamicWidth)
            maxElementWidth = CalculateDynamicWidth();
        UpdateContainer();
    }

    private void UpdateContainer() 
    {
        elementWidth = GetElementWidth();

        int startingIndex = 0;

        if (elementsList.Count > 0) 
        {
            for (int i = 0; i < elementsList.Count; i++)
            {
                if (dataArray.Array.Count > i)
                {
                    SetupElement(dataArray.Array[i], elementsList[i], i);
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
            SetupElement(dataArray.Array[i], elementsList[i], i);
        }
    }

    private void SetupElement(int elementValue, Image element, int elementIndex) 
    {
        element.gameObject.SetActive(true);
        element.rectTransform.sizeDelta = new Vector2(elementWidth, GetElementHight(elementValue));
        element.transform.localPosition = GetElementPosition(elementIndex);
    }

    private void CreateElement()
    {
        //Image element = Instantiate<Image>(new GameObject().GetComponent<Image>());
        GameObject elementObject = Instantiate(imagePrefab);
        elementObject.transform.SetParent(containerRect);
        elementObject.transform.localScale = Vector3.one;
        Image element = elementObject.GetComponent<Image>();
        elementsList.Add(element);
    }

    private float GetElementWidth() 
    {
        float containerWidth = containerRect.rect.width;
        float dynamicWidth = containerWidth / settings.ArraySize;

        if (dynamicWidth > maxElementWidth)
            return maxElementWidth;
        else if (dynamicWidth > minElementWidth)
            return dynamicWidth;
        else
            return minElementWidth;
    }

    private float GetElementHight(int elementValue)
    {
        float result = 0;

        float maxHeight = containerRect.rect.height;
        float elementRatio = (float)elementValue / settings.ArraySize;

        result = maxHeight * elementRatio;

        return result;
    }

    private Vector2 GetElementPosition(int imageIndex) 
    {
        Vector2 result = Vector2.zero;

        if ((maxElementWidth + padding) * settings.ArraySize < containerRect.rect.width)
        {
            result = GetElementPositionEqulibrium(imageIndex);
        }
        else 
        {
            result = GetElementPositionFlat(imageIndex);
        }

        return result;
    }

    private Vector2 GetElementPositionFlat(int imageIndex) 
    {
        float containerWidth = containerRect.rect.width;
        float startingX = containerWidth * 0.5f * -1;
        float xPos = startingX + (padding + elementWidth) * imageIndex + (elementWidth * 0.5f);

        Vector2 result = new Vector2(xPos, containerRect.rect.height * 0.5f * -1);

        return result;
    }

    private Vector2 GetElementPositionEqulibrium(int imageIndex) 
    {
        float containerWidth = containerRect.rect.width;
        float xOffset = 1f / (settings.ArraySize + 1);

        float gap = containerWidth * xOffset;

        float xPos = (containerWidth * 0.5f * -1) + gap * (imageIndex + 1);

        Vector2 result = new Vector2(xPos, containerRect.rect.height * 0.5f * -1);

        return result;
    }

    private float CalculateDynamicWidth() 
    {
        float containerWidth = containerRect.rect.width;
        float result = containerWidth / 40;
        return result;
    }

    public int CalculateMaxArrayNumber() 
    {
        float containerWidth = containerRect.rect.width;
        int maxElements = (int)((containerWidth) / (minElementWidth + padding));

        return maxElements;
    }
}