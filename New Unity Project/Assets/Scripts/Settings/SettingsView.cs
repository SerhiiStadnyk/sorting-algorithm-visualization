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
    }

    public void Dropdown_SetSortingType(int enumId) 
    {
        settings.SetSortingType(enumId);
    }

    public void Dropdown_SetArrayRandomizer(int enumId) 
    {
        settings.SetRandomizerType(enumId);
    }

    public void Slider_ChangeSpeed() 
    {
    }

    public void Slider_ChangeArraySize() 
    {
        arraySizeText.text = arraySizeSlider.value.ToString();
        settings.SetArraySize((int)arraySizeSlider.value);
    }

    public void Button_CreateArray() 
    {
        sortingController.CreateArray();
    }
}