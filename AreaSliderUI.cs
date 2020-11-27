using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Refsa.UI.ColorPicker
{
    public class AreaSliderUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] GUIEvent knob;

        public event System.Action<Vector2> valueChanged;

        public Vector2 NormalizedPosition { get; private set; }

        RectTransform thisTransform;

        void Start()
        {
            thisTransform = GetComponent<RectTransform>();

            knob.mouseDrag += OnKnobDrag;
        }

        void OnKnobDrag(Vector2 delta)
        {
            Vector3 newPos = knob.transform.position + (Vector3)delta;

            TrySetKnob(newPos);
        }

        void TrySetKnob(Vector2 pixelPos)
        {
            var rect = thisTransform.rect;
            rect.size *= thisTransform.lossyScale;
            rect.center = (Vector2)thisTransform.position;

            if (pixelPos.x > rect.xMin && pixelPos.x < rect.xMax)
            {
                knob.transform.position = new Vector3(pixelPos.x, knob.transform.position.y, knob.transform.position.z);
            }
            if (pixelPos.y > rect.yMin && pixelPos.y < rect.yMax)
            {
                knob.transform.position = new Vector3(knob.transform.position.x, pixelPos.y, knob.transform.position.z);
            }

            NormalizedPosition = (knob.transform.localPosition / thisTransform.rect.size) + Vector2.one * 0.5f;
            valueChanged?.Invoke(NormalizedPosition);
        }

        public void SetKnobColor(Color color)
        {
            knob.GetComponent<Image>().color = color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TrySetKnob(eventData.position);
            knob.SetState(GUIEvent.State.Drag);
        }
    }
}