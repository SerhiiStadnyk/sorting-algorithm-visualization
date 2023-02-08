using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VisualizerSettings;

namespace ArrayVisualizer
{
    internal class ColumnVisualizer : VisualizerBase
    {
        private readonly ColumnVisualyzerSettings _columnSettings;

        private bool _dynamicWidth;
        private readonly List<Image> _elementsList = new List<Image>();
        private float _elementWidth;
        private float _maxElementWidth;
        private float _minElementWidth;
        private int _padding;


        public ColumnVisualizer(
            ColumnVisualyzerSettings columnSettings,
            DataArray dataArray,
            RectTransform containerRect,
            Settings settings)
        {
            _columnSettings = columnSettings;
            this.dataArray = dataArray;
            this.containerRect = containerRect;
            this.settings = settings;
            UpdateSettings();
        }


        public override void Build()
        {
            UpdateSettings();
            UpdateContainer();
        }


        public override void UpdateContainer()
        {
            if (_dynamicWidth)
            {
                _maxElementWidth = CalculateDynamicWidth();
            }

            _elementWidth = GetElementWidth();

            int startingIndex = 0;

            if (_elementsList.Count > 0)
            {
                for (int i = 0; i < _elementsList.Count; i++)
                {
                    if (dataArray.Array.Count > i)
                    {
                        SetupElement(dataArray.Array[i], i);
                    }
                    else
                    {
                        _elementsList[i].gameObject.SetActive(false);
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


        public override void UpdateElement(int elementIndex)
        {
            Image element = _elementsList[elementIndex];
            int elementValue = dataArray.Array[elementIndex];
            element.rectTransform.sizeDelta = new Vector2(_elementWidth, GetElementHeight(elementValue));
        }


        public override int CalculateMaxArrayNumber()
        {
            containerRect.ForceUpdateRectTransforms();
            float containerWidth = containerRect.rect.width;

            return (int)(containerWidth / _minElementWidth);
        }


        private void UpdateSettings()
        {
            _minElementWidth = _columnSettings.MinElementWidth;
            _maxElementWidth = _columnSettings.MaxElementWidth;
            _padding = _columnSettings.Padding;
            _dynamicWidth = _columnSettings.DynamicWidth;

            switch (settings.SortingType)
            {
                default:
                    visualizationColoring = new VisualizerColoringStandard(_elementsList);
                    break;
            }
        }


        private void SetupElement(int elementValue, int elementIndex)
        {
            Image element = _elementsList[elementIndex];
            element.gameObject.SetActive(true);
            element.rectTransform.sizeDelta = new Vector2(_elementWidth, GetElementHeight(elementValue));
            element.transform.localPosition = GetElementPosition(elementIndex);
            element.color = Color.white;
        }


        private void CreateElement()
        {
            Image image = new GameObject().AddComponent<Image>();
            image.rectTransform.pivot = new Vector2(0.5f, 0f);

            Transform transform = image.transform;
            transform.SetParent(containerRect);
            transform.localScale = Vector3.one;

            _elementsList.Add(image);
        }


        private float GetElementWidth()
        {
            float result = _minElementWidth;

            float containerWidth = containerRect.rect.width;
            float dynamicWidth = containerWidth / dataArray.Array.Count;

            if (dynamicWidth > _maxElementWidth)
            {
                result = _maxElementWidth;
            }

            if (dynamicWidth > _minElementWidth)
            {
                result = dynamicWidth;
            }

            return result;
        }


        private float GetElementHeight(int elementValue)
        {
            float maxHeight = containerRect.rect.height;
            float elementRatio = (elementValue + 1f) / dataArray.Array.Count;

            return maxHeight * elementRatio;
        }


        private Vector2 GetElementPosition(int imageIndex)
        {
            Vector2 result;

            if ((_maxElementWidth + _padding) * dataArray.Array.Count < containerRect.rect.width)
            {
                result = GetElementPositionEquilibrium(imageIndex);
            }
            else
            {
                result = GetElementPositionFlat(imageIndex);
            }

            return result;
        }


        private Vector2 GetElementPositionFlat(int imageIndex)
        {
            Rect rect = containerRect.rect;
            float containerWidth = rect.width;
            float startingX = containerWidth * 0.5f * -1;
            float xPos = startingX + _elementWidth * imageIndex + _elementWidth * 0.5f;

            Vector2 result = new Vector2(xPos, rect.height * 0.5f * -1);
            return result;
        }


        private Vector2 GetElementPositionEquilibrium(int imageIndex)
        {
            Rect rect = containerRect.rect;
            float containerWidth = rect.width;
            float xOffset = 1f / (dataArray.Array.Count + 1);

            float gap = containerWidth * xOffset;
            float xPos = containerWidth * 0.5f * -1 + gap * (imageIndex + 1);

            Vector2 result = new Vector2(xPos, rect.height * 0.5f * -1);

            return result;
        }


        private float CalculateDynamicWidth()
        {
            float containerWidth = containerRect.rect.width;
            return containerWidth / 40;
        }
    }
}