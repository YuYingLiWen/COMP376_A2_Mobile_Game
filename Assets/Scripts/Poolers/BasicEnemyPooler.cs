using UnityEngine;
using UnityEngine.Pool;

public class BasicEnemyPooler : GameObjectPooler<Enemy>
{
    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
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
    }

    protected override void OnRelease(Enemy obj)
    {
        obj.gameObject.SetActive(false);
    }

    private ObjectPool<BasicEnemyPooler> pool;
    public ObjectPool<BasicEnemyPooler> Pool => pool;

    private static BasicEnemyPooler instance;
    public static BasicEnemyPooler Instance => instance;
}