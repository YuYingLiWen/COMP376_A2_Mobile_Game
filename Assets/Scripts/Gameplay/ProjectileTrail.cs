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
        line.sortingLayerName = "Middle";
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= persistTime) ProjectileTrailPooler.Instance.Pool.Release(this);
        elapsedTime += Time.deltaTime;
    }

    public void SetPositions(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start - Vector3.forward);
        line.SetPosition(1, end - Vector3.forward);
    }

    public void SetWildShot(Vector3 start, Vector3 direction, Vector3 aimCircle)
    {
        line.SetPosition(0, start - Vector3.forward);
        line.SetPosition(1, direction * outOfView + aimCircle - Vector3.forward);
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
