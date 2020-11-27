using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Refsa.UI.ColorPicker
{
    public class SliderUI : SliderUIBase
    {
        protected override void OnAreaClicked(Vector2 pos)
        {
            Vector3 newPos = new Vector3(pos.x, knob.transform.position.y, knob.transform.position.z);

            TrySetKnob(pos);
        }

        protected override void OnKnobDrag(Vector2 delta)
        {
            RectTransform knobTransform = knob.GetComponent<RectTransform>();
            Vector3 newPos = knobTransform.position + new Vector3(delta.x, 0, 0);
            TrySetKnob(newPos);            
        }

        public override void TrySetKnob(Vector2 pixelPos)
        {
            var rect = thisTransform.rect;
            rect.size *= thisTransform.lossyScale;
            rect.center = (Vector2)thisTransform.position;

            if (rect.Contains(pixelPos))
            {
                knobTransform.position = pixelPos;
            }

            Percent = (knobTransform.localPosition.x / thisTransform.sizeDelta.x) + 0.5f;

            knobTransform.localPosition = new Vector3(knobTransform.localPosition.x, 0f, 0f);

            DispatchValueChanged();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnAreaClicked(eventData.pressPosition);
            knob.SetState(GUIEvent.State.Drag);
        }
    }
}