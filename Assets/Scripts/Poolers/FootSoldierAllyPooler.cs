using UnityEngine;
using UnityEngine.Pool;

public class FootSoldierAllyPooler : GameObjectPooler<Ally>
{
    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
    }

    protected override void Start()
    {
        pool = new ObjectPool<Ally>(OnCreate, OnGetFromPool, OnRelease, OnDestruction, collectionCheck, initialAmount);
    }

    protected override Ally OnCreate()
    {
        var newObject = Instantiate(toPool);
        newObject.transform.parent = transform;
        return newObject.GetComponent<Ally>();
    }

    protected override void OnDestruction(Ally obj)
    {
        Destroy(obj.gameObject);
    }

    protected override void OnGetFromPool(Ally obj)
    {
        obj.gameObject.SetActive(true);
        obj.transform.up = Vector3.down;
    }

    protected override void OnRelease(Ally obj)
    {
        obj.gameObject.SetActive(false);
    }

    private ObjectPool<Ally> pool;
    public ObjectPool<Ally> Pool => pool;

    private static FootSoldierAllyPooler instance;
    public static FootSoldierAllyPooler Instance => instance;
}