using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Refsa.UI.ColorPicker
{
    public class AreaSliderUI : SliderUIBase<Vector2>
    {
        protected override void OnKnobDrag(Vector2 delta)
        {
            Vector3 newPos = knob.transform.position + (Vector3)delta;

            TrySetKnob(newPos);
        }

        public override void TrySetKnob(Vector2 pixelPos)
        {
            var rect = container.rect;
            rect.size *= container.lossyScale;
            rect.center = (Vector2)container.position;

            if (pixelPos.x > rect.xMin && pixelPos.x < rect.xMax)
            {
                knob.transform.position = new Vector3(pixelPos.x, knob.transform.position.y, knob.transform.position.z);
            }
            if (pixelPos.y > rect.yMin && pixelPos.y < rect.yMax)
            {
                knob.transform.position = new Vector3(knob.transform.position.x, pixelPos.y, knob.transform.position.z);
            }

            Value = (knob.transform.localPosition / container.rect.size) + Vector2.one * 0.5f;
            DispatchValueChanged();
        }

        public void SetKnobColor(Color color)
        {
            knob.GetComponent<Image>().color = color;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            TrySetKnob(eventData.position);
            knob.SetState(GUIEvent.State.Drag);
        }
    }
}