using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Refsa.UI.ColorPicker
{
    public abstract class SliderUIBase : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] protected GUIEvent knob;

        protected RectTransform thisTransform;
        protected RectTransform knobTransform;

        public float Percent { get; protected set; }

        public event System.Action<float> valueChanged;

        void Start()
        {
            knob.mouseDrag += OnKnobDrag;

            thisTransform = GetComponent<RectTransform>();
            knobTransform = knob.GetComponent<RectTransform>();

            OnKnobDrag(Vector2.zero);
        }

        protected virtual void OnAreaClicked(Vector2 pos)
        {
            Vector3 newPos = new Vector3(pos.x, knob.transform.position.y, knob.transform.position.z);

            TrySetKnob(pos);
        }

        protected virtual void OnKnobDrag(Vector2 delta)
        {
            RectTransform knobTransform = knob.GetComponent<RectTransform>();
            Vector3 newPos = knobTransform.position + new Vector3(delta.x, 0, 0);
            TrySetKnob(newPos);            
        }

        protected void DispatchValueChanged()
        {
            valueChanged?.Invoke(Percent);
        }

        public abstract void TrySetKnob(Vector2 pixelPos);
        public abstract void OnPointerDown(PointerEventData eventData);
    }
}