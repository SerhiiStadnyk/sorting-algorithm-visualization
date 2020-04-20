using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown sortingTypeDropDown = null;
    [SerializeField] private Settings settings = null;

    private void Start()
    {
        string[] sortingList = Enum.GetNames(typeof(SortingTypes));
        List<string> names = new List<string>(sortingList);
        sortingTypeDropDown.AddOptions(names);
    }

    public void Dropdown_SetSortingType(int enumId) 
    {
        settings.SetSortingType(enumId);
    }

    public void Dropdown_SetArrayRandomizer(int enumId) 
    {
    }
}