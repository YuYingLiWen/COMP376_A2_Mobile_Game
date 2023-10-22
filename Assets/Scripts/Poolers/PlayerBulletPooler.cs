using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletPooler : GameObjectPooler<Projectile>
{

    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple PlayerBulletPooler!", this);
    }

    private void Start()
    {
        pool = new ObjectPool<Projectile>(OnCreate, OnGetFromPool, OnRelease, OnDestruction, collectionCheck, initialAmount);
    }

    protected override Projectile OnCreate()
    {
        var newObject = Instantiate(toPool);
        newObject.transform.parent = transform;
        return newObject.GetComponent<Projectile>();
    }

    protected override void OnGetFromPool(Projectile obj)
    {
        obj.gameObject.SetActive(true);
        obj.ResestTime();
    }

    protected override void OnRelease(Projectile obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected override void OnDestruction(Projectile obj)
    {
        Destroy(obj.gameObject);
    }

    private ObjectPool<Projectile> pool;
    public ObjectPool<Projectile> Pool => pool;

    private static PlayerBulletPooler instance;
    public static PlayerBulletPooler Instance => instance;
}