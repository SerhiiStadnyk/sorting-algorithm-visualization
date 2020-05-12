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
    [SerializeField] private TMP_InputField delay;
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
        delay.text = settings.Delay.ToString();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
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

    public void Input_Delay(string value) 
    {
        int tmp;

        if(int.TryParse(value, out tmp) == false)
            tmp = 0;

        settings.SetDelay(tmp);
        delay.text = settings.Delay.ToString();
    }

    public void Input_TactsPerFrame(string value) 
    {
        int tmp;

        if (int.TryParse(value, out tmp) == false)
            tmp = 0;
        settings.SetSortingTactsPerFrame(tmp);
        tactsPerFrame.text = settings.SortingTactsPerFrame.ToString();
    }
}