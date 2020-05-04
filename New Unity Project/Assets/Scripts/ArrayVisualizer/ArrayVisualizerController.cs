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
    private List<Image> markedElements = new List<Image>();
    private Image elementMain;
    private Image elementToCompare;
    private List<int> marksList = new List<int>();

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

    private void SetupElement(int elementValue, int elementIndex) 
    {
        Image element = elementsList[elementIndex];
        element.gameObject.SetActive(true);
        element.rectTransform.sizeDelta = new Vector2(elementWidth, GetElementHight(elementValue));
        element.transform.localPosition = GetElementPosition(elementIndex);
        element.color = Color.white;
    }

    public void UpdateElement(int elementIndex) 
    {
        Image element = elementsList[elementIndex];
        int elementValue = dataArray.Array[elementIndex];
        element.rectTransform.sizeDelta = new Vector2(elementWidth, GetElementHight(elementValue));
    }
    public void SwitchElements(int fromIndex, int toIndex) 
    {
        Image firstElement = elementsList[fromIndex];
        Image secondElement = elementsList[toIndex];
        float tmpHight = secondElement.rectTransform.rect.height;
        secondElement.rectTransform.sizeDelta = new Vector2(elementWidth, firstElement.rectTransform.rect.height);
        firstElement.rectTransform.sizeDelta = new Vector2(elementWidth, tmpHight);
    }

    private void CreateElement()
    {
        GameObject elementObject = Instantiate(imagePrefab);
        elementObject.transform.SetParent(containerRect);
        elementObject.transform.localScale = Vector3.one;
        Image element = elementObject.GetComponent<Image>();
        elementsList.Add(element);
    }

    public void RemoveMarks() 
    {
        elementsList.ForEach(image => image.color = Color.white);
    }
    public void MarkElements() 
    {
        if (markedElements.Count > 0) 
        {
            markedElements.ForEach(image => image.color = Color.white);
            markedElements.Clear();
        }

        for (int i = 0; i < marksList.Count; i++)
        {
            markedElements.Add(elementsList[marksList[i]]);
            elementsList[marksList[i]].color = Color.red;
        }

        marksList.Clear();
    }
    public void AddMarks(params int[] indexArray) 
    {
        for (int i = 0; i < indexArray.Length; i++)
        {
            marksList.Add(indexArray[i]);
        }
    }

    public void MarkForCheck(int index, bool isWrong) 
    {
        if (isWrong)
            elementsList[index].color = Color.red;
        else
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