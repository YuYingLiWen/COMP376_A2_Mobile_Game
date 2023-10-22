using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class VirtualJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,  IPointerExitHandler
{
    [SerializeField] private float dragRadius = 50.0f;
    [SerializeField] private bool resetAxisOnExit = true;

    private Vector2 anchor;

    Vector2 center;

    private Vector2 axis = Vector2.zero;

    /// <summary>
    ///  The X, Y position for the joystick.
    /// </summary>
    public Vector2 GetAxis() => axis;


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

        Debug.Log("AXIS: " + axis);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = anchor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //To get OnPointerUp() to work as per documentation
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(resetAxisOnExit) axis = Vector3.zero;
    }
}