using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VisualizerSettings
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField]
        private Settings _settings;

        [SerializeField]
        private TMP_Dropdown _sortingTypeDropDown;

        [SerializeField]
        private TMP_Dropdown _randomzierTypeDropDown;

        [SerializeField]
        private SortingController _sortingController;

        [SerializeField]
        private TMP_Text _arraySizeText;

        [SerializeField]
        private TMP_InputField _delay;

        [SerializeField]
        private TMP_InputField _tactsPerFrame;

        public Slider _arraySizeSlider;


        public void DropdownSetSortingType(int enumId)
        {
            _settings.SetSortingType(enumId);
        }


        public void DropdownSetArrayRandomizer(int enumId)
        {
            _settings.SetRandomizerType(enumId);
        }


        public void SliderChangeArraySize()
        {
            _arraySizeText.text = _arraySizeSlider.value.ToString();
            _settings.SetArraySize((int)_arraySizeSlider.value);
        }


        public void ButtonCreateArray()
        {
            if (_sortingController.IsSorting)
            {
                return;
            }

            _sortingController.CreateArray();
            InputTactsPerFrame(_settings.SortingTactsPerFrame.ToString());
        }


        public void InputDelay(string value)
        {
            if (int.TryParse(value, out int tmp) == false)
            {
                tmp = 0;
            }

            _settings.SetDelay(tmp);
            _delay.text = _settings.Delay.ToString();
        }


        public void InputTactsPerFrame(string value)
        {
            if (int.TryParse(value, out int tmp) == false)
            {
                tmp = 0;
            }

            _settings.SetSortingTactsPerFrame(tmp);
            _tactsPerFrame.text = _settings.SortingTactsPerFrame.ToString();
        }


        protected void Start()
        {
            string[] sortingList = Enum.GetNames(typeof(SortingTypes));
            var names = new List<string>(sortingList);
            _sortingTypeDropDown.AddOptions(names);
            _sortingTypeDropDown.value = (int)_settings.SortingType;

            string[] randomizerList = Enum.GetNames(typeof(RandomizerTypes));
            var randomizerNames = new List<string>(randomizerList);
            _randomzierTypeDropDown.AddOptions(randomizerNames);
            _randomzierTypeDropDown.value = (int)_settings.RandomizerType;

            _tactsPerFrame.text = _settings.SortingTactsPerFrame.ToString();
            _delay.text = _settings.Delay.ToString();
        }
    }
}