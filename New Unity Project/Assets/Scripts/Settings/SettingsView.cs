using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Settings settings;
    [SerializeField] private TMP_Dropdown sortingTypeDropDown;
    [SerializeField] private TMP_Dropdown randomzierTypeDropDown;
    [SerializeField] private SortingController sortingController;
    [SerializeField] private TMP_Text arraySizeText;
    [SerializeField] private TMP_InputField maxFps;
    [SerializeField] private TMP_InputField tactsPerFrame;
    public Slider arraySizeSlider;
#pragma warning restore 0649

    private void Start()
    {
        string[] sortingList = Enum.GetNames(typeof(SortingTypes));
        List<string> names = new List<string>(sortingList);
        sortingTypeDropDown.AddOptions(names);
        sortingTypeDropDown.value = (int)settings.SortingType;

        string[] randomizerList = Enum.GetNames(typeof(RandomizerTypes));
        List<string> randomizerNames = new List<string>(randomizerList);
        randomzierTypeDropDown.AddOptions(randomizerNames);
        randomzierTypeDropDown.value = (int)settings.RandomizerType;

        tactsPerFrame.text = settings.SortingTactsPerFrame.ToString();
        maxFps.text = settings.MaxFps.ToString();
    }

    public void Dropdown_SetSortingType(int enumId) 
    {
        settings.SetSortingType(enumId);
    }

    public void Dropdown_SetArrayRandomizer(int enumId) 
    {
        settings.SetRandomizerType(enumId);
    }

    public void Slider_ChangeArraySize() 
    {
        arraySizeText.text = arraySizeSlider.value.ToString();
        settings.SetArraySize((int)arraySizeSlider.value);
    }

    public void Button_CreateArray() 
    {
        sortingController.CreateArray();
        Input_TactsPerFrame(settings.SortingTactsPerFrame.ToString());
    }

    public void Input_MaxFps(string value) 
    {
        int tmp;
        if (string.IsNullOrEmpty(value))
            tmp = 0;
        else
            tmp = int.Parse(value);
        settings.SetMaxFps(tmp);
        maxFps.text = settings.MaxFps.ToString();
    }

    public void Input_TactsPerFrame(string value) 
    {
        int tmp;
        if (string.IsNullOrEmpty(value))
            tmp = 0;
        else
            tmp = int.Parse(value);
        settings.SetSortingTactsPerFrame(tmp);
        tactsPerFrame.text = settings.SortingTactsPerFrame.ToString();
    }
}