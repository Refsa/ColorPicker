using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIEvent : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [System.Flags]
    public enum State : uint
    {
        None = 0u,
        All = ~0u,
        Down = 1u << 0,
        Drag = 1u << 2,
        Over = 1u << 3
    }

    public event System.Action mouseDown;
    public event System.Action mouseUp;
    public event System.Action mouseEnter;
    public event System.Action mouseLeave;

    public event System.Action<Vector2> mouseDrag;

    State state;
    public State CurrentState => state;

    Vector3 lastMousePosition;
    Vector2 mouseDelta;

    public void OnPointerDown(PointerEventData eventData)
    {
        state |= State.Down | State.Drag;
        mouseDown?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        state &= ~State.Down;
        state &= ~State.Drag;
        mouseUp?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        state |= State.Over;
        mouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        state &= ~State.Over;
        mouseLeave?.Invoke();
    }

    void Update() 
    {
        mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        if ((state & State.Drag) != 0)
        {
            mouseDrag?.Invoke(mouseDelta);
        }

        if ((state & State.Drag) != 0 && (state & State.Over) == 0 && Input.GetKeyUp(KeyCode.Mouse0))
        {
            state = State.None;
        }
    }
}
