using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderUI : MonoBehaviour
{
    [SerializeField] GUIEvent knob;

    RectTransform thisTransform;

    public float Percent {get; private set;}

    public event System.Action<float> sliderValueChanged;

    void Start()
    {
        knob.mouseDrag += OnKnobDrag;
        thisTransform = GetComponent<RectTransform>();
    }

    void OnKnobDrag(Vector2 delta)
    {
        RectTransform knobTransform = knob.GetComponent<RectTransform>();

        Vector3 newPos = knobTransform.position + new Vector3(delta.x, 0, 0);

        var rect = thisTransform.rect;
        rect.size *= 1.5f;
        rect.center = (Vector2)thisTransform.position;

        if (rect.Contains(newPos))
        {
            knobTransform.position = newPos;
        }

        Percent = (knobTransform.localPosition.x / thisTransform.sizeDelta.x) + 0.5f;

        sliderValueChanged?.Invoke(Percent);
    }
}
