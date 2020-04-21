using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayVisualizerController : MonoBehaviour
{
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private HorizontalLayoutGroup horizontalLayout;

    [SerializeField] private GameObject imagePrefab;

    [SerializeField] private int minElementWidth = 3;
    [SerializeField] private int maxElementWidth = 25;
    [SerializeField] private int padding = 5;

    private Settings settings;
    private DataArray dataArray;
    private List<Image> elementsList = new List<Image>();

    public void Init(Settings settings, DataArray dataArray) 
    {
        this.settings = settings;
        this.dataArray = dataArray;
        settings.SetMaximumArraySize(CalculateMaxArrayNumber());
        UpdateContainer();
    }

    private void UpdateContainer() 
    {
        int startingIndex = 0;

        if (elementsList.Count > 0) 
        {
            for (int i = 0; i < elementsList.Count; i++)
            {
                if (dataArray.Array.Count > i)
                {
                    SetupElement(dataArray.Array[i], elementsList[i]);
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
            SetupElement(dataArray.Array[i], elementsList[i]);
        }
    }

    private void SetupElement(int elementValue, Image element) 
    {
        element.gameObject.SetActive(true);
        element.rectTransform.sizeDelta = new Vector2(GetElementWidth(), GetElementHight(elementValue));
    }

    private void CreateElement()
    {
        //Image element = Instantiate<Image>(new GameObject().GetComponent<Image>());
        GameObject elementObject = Instantiate(imagePrefab);
        elementObject.transform.SetParent(containerRect);
        elementObject.transform.localScale = Vector3.one;
        Image element = elementObject.GetComponent<Image>();
        elementsList.Add(element);

        Debug.Log("Element created");
    }

    private float GetElementWidth() 
    {
        float containerWidth = containerRect.rect.width;
        float paddingSize = padding * settings.ArraySize;
        float freeWidth = (containerWidth - paddingSize) / (settings.ArraySize * minElementWidth);

        if (freeWidth > maxElementWidth)
            return maxElementWidth;
        else if (freeWidth > minElementWidth)
            return freeWidth;
        else
            return minElementWidth;
    }

    private float GetElementHight(int elementValue)
    {
        float result = 0;

        float maxHeight = containerRect.rect.height;
        Debug.LogWarning("Max height: " + maxHeight);
        float elementRatio = (float)elementValue / settings.ArraySize;
        Debug.LogWarning("elementValue: " + elementValue);
        Debug.LogWarning("settings.ArraySize: " + settings.ArraySize);
        Debug.LogWarning("elementRatio: " + elementRatio);

        result = maxHeight * elementRatio;

        return result;
    }

    public int CalculateMaxArrayNumber() 
    {
        float containerWidth = containerRect.rect.width;
        float paddingSize = settings.ArraySize;
        int maxElements = (int)(containerWidth - paddingSize) / (minElementWidth + padding);

        return maxElements;
    }
}