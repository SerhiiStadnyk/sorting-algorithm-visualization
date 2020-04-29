using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayVisualizerController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private RectTransform containerRect;

    [SerializeField] private GameObject imagePrefab;

    [SerializeField] private float minElementWidth = 3;
    [SerializeField] private float maxElementWidth = 25;
    [SerializeField] private int padding = 5;

    [SerializeField] private bool dynamicWidth = false;
#pragma warning restore 0649

    private DataArray dataArray;
    private List<Image> elementsList = new List<Image>();
    private Image elementMain;
    private Image elementToCompare;

    private float elementWidth;

    public void Init(DataArray dataArray) 
    {
        this.dataArray = dataArray;
        if(dynamicWidth)
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
        element.color = Color.white;
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

    public void RemoveMarks() 
    {
        if (elementMain != null)
            elementMain.color = Color.white;
        elementMain = null;
        if (elementToCompare != null)
            elementToCompare.color = Color.white;
        elementToCompare = null;
    }
    public void MarkMainElemet(int index) 
    {
        MarkElement(index, elementMain);
    }
    public void MarkSecondaryElemet(int index)
    {
        MarkElement(index, elementToCompare);
    }
    private void MarkElement(int index, Image image) 
    {
        if (image != null)
            image.color = Color.white;

        image = elementsList[index];
        image.color = Color.red;
    }
    public void MarkForCheck(int index) 
    {
        elementsList[index].color = Color.green;
    }

    private float GetElementWidth() 
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

    private float GetElementHight(int elementValue)
    {
        float result = 0;

        float maxHeight = containerRect.rect.height;
        float elementRatio = ((float)elementValue + 1f) / dataArray.Array.Count;

        result = maxHeight * elementRatio;

        return result;
    }

    private Vector2 GetElementPosition(int imageIndex) 
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
        float xOffset = 1f / (dataArray.Array.Count + 1);

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