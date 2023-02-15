using System.Collections;
using System.Collections.Generic;
using ArrayVisualizer;
using SortingAlgorithms;
using TMPro;
using UnityEngine;
using VisualizerSettings;

[RequireComponent(typeof(ArrayVisualizerController))]
public class SortingController : MonoBehaviour, ISortingHandable
{
    [SerializeField]
    private Settings _settings;

    [SerializeField]
    private SettingsView _settingsView;

    [SerializeField]
    private DataArray _dataArray;

    [SerializeField]
    private TMP_Text _arrayAccesInfo;

    [SerializeField]
    private TMP_Text _comparesInfo;

    [SerializeField]
    private TMP_Text _timeInfo;

    private ArrayVisualizerController _arrayVisualizer;
    private Coroutine _checkingCoroutine;

    private SortingAlgorithmBase _sortingAlgorithm;

    private Coroutine _sortingCoroutine;

    private List<int> _tmpArray;

    private int _calculationPerFrameCounter = 0;
    private float _time;

    public bool IsSorting { get; private set; }

    public List<int> Array
    {
        get => _dataArray.Array;
        set => _dataArray.Array = value;
    }


    public bool IsVisualUpdateNeeded => _calculationPerFrameCounter >= _settings.SortingTactsPerFrame;

    public bool CanProceedSorting => !IsVisualUpdateNeeded;


    public void RelocateElements(int fromIndex, int toIndex)
    {
        TextAddValue(_arrayAccesInfo);

        _arrayVisualizer.UpdateElement(fromIndex);
        _arrayVisualizer.UpdateElement(toIndex);
    }


    public void FinishSorting()
    {
        if (_sortingCoroutine != null)
        {
            StopCoroutine(_sortingCoroutine);
        }

        if (_checkingCoroutine != null)
        {
            StopCoroutine(_checkingCoroutine);
        }

        _checkingCoroutine = StartCoroutine(CheckData());
    }


    public void MarkElements(bool count = false, params ElementColor[] markedElements)
    {
        _calculationPerFrameCounter++;

        if (count)
        {
            TextAddValue(_comparesInfo, markedElements.Length);
        }

        _arrayVisualizer.AddMarks(markedElements);

        if (IsVisualUpdateNeeded)
        {
            StartCoroutine(UpdateVisualizer());
        }
    }


    public void CreateArray()
    {
        _dataArray.CreateArray(_settings.ArraySize, _settings.RandomizerType);
        _arrayVisualizer.Build();
    }


    public void StartSorting()
    {
        if (IsSorting)
        {
            return;
        }

        if (_dataArray.Array == null || _dataArray.Array.Count == 0)
        {
            return;
        }

        IsSorting = true;
        _tmpArray = new List<int>(_dataArray.Array);

        CleanUp();

        _arrayAccesInfo.text = "0";
        _comparesInfo.text = "0";
        _timeInfo.text = "0";

        _sortingAlgorithm = SortingAlgorithmCreator.GetAlgorithm(this, _settings.SortingType);

        if (_checkingCoroutine != null)
        {
            StopCoroutine(_checkingCoroutine);
        }

        if (_sortingCoroutine != null)
        {
            StopCoroutine(_sortingCoroutine);
        }

        _sortingCoroutine = StartCoroutine(StateSorting());
    }


    public void Button_CheckData()
    {
        if (IsSorting)
        {
            return;
        }

        CleanUp();
        if (_checkingCoroutine != null)
        {
            StopCoroutine(_checkingCoroutine);
        }

        _checkingCoroutine = StartCoroutine(CheckData());
    }


    public void ButtonReset()
    {
        IsSorting = false;

        if (_checkingCoroutine != null)
        {
            StopCoroutine(_checkingCoroutine);
        }

        if (_sortingCoroutine != null)
        {
            StopCoroutine(_sortingCoroutine);
        }

        _dataArray.Array = _tmpArray;
        CleanUp();

        _arrayVisualizer.Build();
    }


    private void Awake()
    {
        _dataArray = new DataArray();
        _arrayVisualizer = GetComponent<ArrayVisualizerController>();
    }


    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        _arrayVisualizer.Init(VisualizerTypes.Column, _dataArray);

        _settings.SetMaximumArraySize(_arrayVisualizer.CalculateMaxArraySize());
        _settingsView._arraySizeSlider.maxValue = _settings.MaxArraySize;
        _settingsView._arraySizeSlider.value = _settings.ArraySize;
    }


    private void CleanUp()
    {
        _sortingAlgorithm = null;
        _arrayVisualizer.RemoveMarks();
    }


    private void TextAddValue(TMP_Text text, float value = 1)
    {
        float tmp = float.Parse(text.text);
        tmp += value;
        text.text = (tmp + value).ToString();
    }


    private IEnumerator StateSorting()
    {
        _time = Time.realtimeSinceStartup;
        yield return _sortingAlgorithm.Sort();
        _timeInfo.text = (Time.realtimeSinceStartup - _time).ToString();
    }


    public IEnumerator UpdateVisualizer()
    {
        if (!IsSorting)
        {
            yield break;
        }

        _arrayVisualizer.MarkElements();
        yield return new WaitForSeconds(_settings.Delay / 1000f);

        _timeInfo.text = (Time.realtimeSinceStartup - _time).ToString();
        _calculationPerFrameCounter = 0;
    }


    private IEnumerator CheckData()
    {
        _calculationPerFrameCounter = 0;
        _arrayVisualizer.RemoveMarks();
        for (int i = 0; i < _dataArray.Array.Count; i++)
        {
            if (_dataArray.Array[i] != i)
            {
                _arrayVisualizer.MarkForCheck(i, true);
            }
            else
            {
                _arrayVisualizer.MarkForCheck(i, false);
            }

            if (_calculationPerFrameCounter == Array.Count / 60)
            {
                _calculationPerFrameCounter = 0;
                yield return null;
            }

            _calculationPerFrameCounter++;
        }

        IsSorting = false;
    }
}