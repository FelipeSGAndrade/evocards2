using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnDragStart = new UnityEvent();
    [SerializeField]
    UnityEvent<bool> OnDragEnd = new UnityEvent<bool>();

    private bool dragging = false;
    private int draggingMouseButton;
    private Vector2 mouseStartDrag;
    private Vector2 mouseLastDrag;

    void Update() {
        if (dragging) {
            Vector2 mousePosition = MouseUtils.GetMousePosition();
            transform.Translate(mousePosition - mouseLastDrag);
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            mouseLastDrag = mousePosition;
        } 
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            StartDrag(0);
        }

        if (dragging && !Input.GetMouseButton(draggingMouseButton)) {
            StopDrag();
        }
    }

    public void StartDrag(int mouseButton) {
        dragging = true;
        draggingMouseButton = mouseButton;
        mouseStartDrag = MouseUtils.GetMousePosition();
        mouseLastDrag = mouseStartDrag;
        OnDragStart.Invoke();
    }

    void StopDrag() {
        dragging = false;
        OnDragEnd.Invoke(HasPositionChanged());
    }

    private bool HasPositionChanged() {
        Vector2 currentMousePosition = MouseUtils.GetMousePosition();
        return mouseStartDrag != currentMousePosition;
    }
}
