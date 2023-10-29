
using System;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class VirtualJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,  IPointerExitHandler
{
    [SerializeField] private float dragRadius = 50.0f;
    [SerializeField] private bool resetAxisOnExit = true;

    public Action OnHoldingStart;
    public Action OnHoldingStop;

    private bool isHolding = false;
    public bool IsHolding => isHolding;

    private Vector2 anchor;

    Vector2 center;

    private Vector2 axis = Vector2.zero;

    /// <summary>
    ///  The X, Y position for the joystick.
    /// </summary>
    public Vector2 GetAxis()
    {
        //Debug.Log(gameObject.name + " : "+ axis);
        return axis;
    }

    private void Awake()
    {
        anchor = transform.position;
        center = (Vector2)transform.localPosition;
    }

    public void HandleMove(Vector2 position)
    {
        var direction = position - anchor;

        if (direction.magnitude <= dragRadius)
        {
            transform.position = position;
        }
        else
        {
            transform.position = anchor + direction.normalized * dragRadius;
        }

        axis = ((Vector2)transform.localPosition - center) / center.magnitude;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = anchor;
        isHolding = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //To get OnPointerUp() to work as per documentation
        isHolding = true;
        OnHoldingStart.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(resetAxisOnExit) axis = Vector3.zero;
        isHolding = false;
        OnHoldingStop.Invoke();
    }
}