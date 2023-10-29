using UnityEngine;
using UnityEngine.Pool;

public class FootSoldierEnemyPooler : GameObjectPooler<Enemy>
{
    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
    }

    protected override void Start()
    {
        pool = new ObjectPool<Enemy>(OnCreate, OnGetFromPool, OnRelease, OnDestruction, collectionCheck, initialAmount);
    }

    protected override Enemy OnCreate()
    {
        var newObject = Instantiate(toPool);
        newObject.transform.parent = transform;
        return newObject.GetComponent<Enemy>();
    }

    protected override void OnDestruction(Enemy obj)
    {
        Destroy(obj.gameObject);
    }

    protected override void OnGetFromPool(Enemy obj)
    {
        obj.gameObject.SetActive(true);
        obj.transform.up = Vector3.down;
    }

    protected override void OnRelease(Enemy obj)
    {
        obj.gameObject.SetActive(false);
    }

    private ObjectPool<Enemy> pool;
    public ObjectPool<Enemy> Pool => pool;

    private static FootSoldierEnemyPooler instance;
    public static FootSoldierEnemyPooler Instance => instance;
}
