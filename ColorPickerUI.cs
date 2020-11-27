using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Refsa.UI.ColorPicker
{
    public class ColorPickerUI : MonoBehaviour
    {
        [SerializeField] AreaSliderUI colorPicker;
        [SerializeField] SliderUI hueSlider;

        Color selectedColor;

        void Start()
        {
            colorPicker.valueChanged += (value) =>
            {
                selectedColor = Color.HSVToRGB(hueSlider.Percent, value.x, value.y);
                colorPicker.SetKnobColor(selectedColor);
            };

            hueSlider.valueChanged += (percent) =>
            {
                colorPicker.SetKnobColor(Color.HSVToRGB(percent, colorPicker.NormalizedPosition.x, colorPicker.NormalizedPosition.y));
                colorPicker.GetComponent<RawImage>().material.SetFloat("_Hue", percent);
            };
        }

        private void Update()
        {

        }
    }
}