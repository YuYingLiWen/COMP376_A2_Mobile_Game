using UnityEngine;

public class ProjectileTrail : MonoBehaviour
{

    [SerializeField] LineRenderer line;

    const float outOfView = 1000.0f;

    [SerializeField] float persistTime = 10.0f;

    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= persistTime) ProjectileTrailPooler.Instance.Pool.Release(this);
        elapsedTime += Time.deltaTime;
    }

    public void SetPositions(Vector2 start, Vector2 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.sortingLayerName = "Ground";
    }

    public void SetWildShot(Vector2 start, Vector2 direction, Vector2 aimCircle)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, direction * outOfView + aimCircle);
        line.sortingLayerName = "Middle";
    }

    public void ResetPositions()
    {
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
    }

    public void ResetTime()
    {
        elapsedTime = 0.0f;
    }
}
