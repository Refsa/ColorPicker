using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Refsa.UI.ColorPicker
{
    public abstract class SliderUIBase<T> : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] protected RectTransform container;
        [SerializeField] protected GUIEvent knob;

        protected RectTransform knobTransform;

        public T Value { get; protected set; }

        public event System.Action<T> valueChanged;

        void Start()
        {
            knob.mouseDrag += OnKnobDrag;

            if (container == null)
            {
                Debug.LogException(new System.ArgumentNullException($"Container of {this.GetType().Name} is null"), this.gameObject);
            }

            knobTransform = knob.GetComponent<RectTransform>();

            OnKnobDrag(Vector2.zero);
        }

        protected virtual void OnAreaClicked(Vector2 pos) { }
        protected virtual void OnKnobDrag(Vector2 delta) { }

        protected void DispatchValueChanged()
        {
            valueChanged?.Invoke(Value);
        }

        public abstract void TrySetKnob(Vector2 pixelPos);
        public abstract void OnPointerDown(PointerEventData eventData);
    }
}