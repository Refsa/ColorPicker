using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Refsa.UI.ColorPicker
{
    public class RadialSliderUI : SliderUIBase
    {
        protected override void OnKnobDrag(Vector2 delta)
        {
            TrySetKnob(Input.mousePosition);
        }

        public override void TrySetKnob(Vector2 pos)
        {
            Vector2 mouseDir = (pos - (Vector2)thisTransform.position).normalized;

            float sangle = Vector2.SignedAngle(mouseDir, Vector2.up) * -1;
            float offset = knobTransform.localPosition.magnitude;
            knobTransform.localPosition = Quaternion.Euler(0f, 0f, sangle) * Vector2.up * offset;
            knobTransform.rotation = Quaternion.Euler(0f, 0f, sangle);

            sangle -= 90f;
            if (sangle < -180f) sangle += 360f;
            Percent = (sangle / 360f + 0.5f);
            DispatchValueChanged();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            TrySetKnob(eventData.position);
            knob.SetState(GUIEvent.State.Drag);
        }
    }
}