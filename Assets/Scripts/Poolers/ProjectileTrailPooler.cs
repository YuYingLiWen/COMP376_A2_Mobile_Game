using UnityEngine;
using UnityEngine.Pool;

public class ProjectileTrailPooler : GameObjectPooler<ProjectileTrail>
{

    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
    }

    protected override void Start()
    {
        pool = new ObjectPool<ProjectileTrail>(OnCreate, OnGetFromPool, OnRelease, OnDestruction, collectionCheck, initialAmount);
    }

    protected override ProjectileTrail OnCreate()
    {
        var newObject = Instantiate(toPool);
        newObject.transform.parent = transform;
        return newObject.GetComponent<ProjectileTrail>();
    }

    protected override void OnGetFromPool(ProjectileTrail obj)
    {
        obj.ResetTime();
        obj.ResetPositions();
        obj.gameObject.SetActive(true);
    }

    protected override void OnRelease(ProjectileTrail obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected override void OnDestruction(ProjectileTrail obj)
    {
        Destroy(obj.gameObject);
    }

    private ObjectPool<ProjectileTrail> pool;
    public ObjectPool<ProjectileTrail> Pool => pool;

    private static ProjectileTrailPooler instance;
    public static ProjectileTrailPooler Instance => instance;
}
