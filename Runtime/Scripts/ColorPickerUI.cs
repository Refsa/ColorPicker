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
        [SerializeField] SliderUIBase hueSlider;

        Color selectedColor;

        public Color SelectedColor => selectedColor;

        public event System.Action<Color> colorChanged;

        void Start()
        {
            colorPicker.valueChanged += (value) =>
            {
                selectedColor = Color.HSVToRGB(hueSlider.Percent, value.x, value.y);
                colorPicker.SetKnobColor(selectedColor);
                
                colorChanged?.Invoke(selectedColor);
            };

            hueSlider.valueChanged += (percent) =>
            {
                selectedColor = Color.HSVToRGB(percent, colorPicker.NormalizedPosition.x, colorPicker.NormalizedPosition.y);
                colorPicker.SetKnobColor(selectedColor);
                colorPicker.GetComponent<RawImage>().materialForRendering.SetFloat("_Hue", percent);

                colorChanged?.Invoke(selectedColor);
            };
        }
    }
}