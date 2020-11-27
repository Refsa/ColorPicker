using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Refsa.UI.ColorPicker
{
    public class ColorPickerUI : MonoBehaviour
    {
        [SerializeField] RectTransform colorPicker;
        [SerializeField] RectTransform huePicker;

        [SerializeField] GUIEvent colorPickerEvent;
        [SerializeField] SliderUI hueSlider;

        Vector2 colorPickerNormalizedPosition;

        Color selectedColor;

        void Start()
        {
            colorPickerEvent.mouseDrag += (delta) =>
            {
                Vector3 newPos = colorPickerEvent.transform.position + (Vector3)delta;

                var rect = colorPicker.rect;
                rect.size *= 1.5f;
                rect.center = (Vector2)colorPicker.position;

                if (newPos.x > rect.xMin && newPos.x < rect.xMax)
                {
                    colorPickerEvent.transform.position += new Vector3(delta.x, 0f, 0f);
                }
                if (newPos.y > rect.yMin && newPos.y < rect.yMax)
                {
                    colorPickerEvent.transform.position += new Vector3(0f, delta.y, 0f);
                }

                colorPickerNormalizedPosition = (colorPickerEvent.transform.localPosition / colorPicker.rect.size) + Vector2.one * 0.5f;

                selectedColor = Color.HSVToRGB(hueSlider.Percent, colorPickerNormalizedPosition.x, colorPickerNormalizedPosition.y);
                colorPickerEvent.GetComponent<Image>().color = selectedColor;
            };

            hueSlider.sliderValueChanged += (percent) =>
            {
                colorPickerEvent.GetComponent<Image>().color = Color.HSVToRGB(percent, colorPickerNormalizedPosition.x, colorPickerNormalizedPosition.y);
                colorPicker.GetComponent<RawImage>().material.SetFloat("_Hue", percent);
            };
        }

        private void Update()
        {

        }
    }
}