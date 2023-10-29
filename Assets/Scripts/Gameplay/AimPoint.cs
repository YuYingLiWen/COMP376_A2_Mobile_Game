
using UnityEngine;

public class AimPoint : MonoBehaviour
{
    const float maxRayDistance = 500.0f;

    private int layerMask;

    [SerializeField] private string[] layerMasks = { "Enemies" };

    private float halfWidth, halfHeight;

    private void Awake()
    {
        layerMask = LayerMask.GetMask(layerMasks);

        halfHeight = Screen.height / 2.0f;
        halfWidth = Screen.width / 2.0f;
    }

    public Vector2 WorldPoint => Camera.main.ScreenToWorldPoint(transform.position);

    public void Translate(Vector2 direction, in float speed)
    {
        Vector3 move = direction * speed * Time.deltaTime;
        Vector3 nextPos = transform.position + move;

        if (nextPos.y <= Screen.height && nextPos.y >= 0.0f &&
            nextPos.x <= Screen.width && nextPos.x >= 0.0f)
        {
            transform.Translate(move);
        }
    }

    public RaycastHit2D Raycast(Vector2 origin, Vector2 aimCircle)
    {
        Vector2 direction = WorldPoint - origin;
        Debug.DrawRay(origin, direction + aimCircle, Color.red, 5.0f);
        return Physics2D.Raycast(origin, direction + aimCircle, direction.magnitude + aimCircle.magnitude, layerMask);
    }

    /// Give a postion in world, set this object to its respective rect position
    public void SetRectPosition(Vector3 position)
    {
        transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
