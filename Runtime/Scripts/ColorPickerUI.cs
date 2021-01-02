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

        public Color SelectedColor => selectedColor;

        public event System.Action<Color> colorChanged;

        void Start()
        {
            colorPicker.valueChanged += (value) =>
            {
                selectedColor = Color.HSVToRGB(hueSlider.Value, value.x, value.y);
                colorPicker.SetKnobColor(selectedColor);
                
                colorChanged?.Invoke(selectedColor);
            };

            hueSlider.valueChanged += (percent) =>
            {
                selectedColor = Color.HSVToRGB(percent, colorPicker.Value.x, colorPicker.Value.y);
                colorPicker.SetKnobColor(selectedColor);
                colorPicker.GetComponent<RawImage>().materialForRendering.SetFloat("_Hue", percent);

                colorChanged?.Invoke(selectedColor);
            };
        }
    }
}