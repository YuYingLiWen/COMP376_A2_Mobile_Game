using UnityEngine;

public abstract class GameObjectPooler<T> : MonoBehaviour
{
    [SerializeField] protected GameObject toPool;
    [SerializeField] protected int initialAmount = 50;
    [SerializeField] protected bool collectionCheck = false;


    /// <summary>
    /// Create pool.
    /// </summary>
    protected abstract void Start();

    /// <summary>
    /// Called when no more inactive object in pool.
    /// </summary>
    protected abstract T OnCreate();

    /// <summary>
    /// Called when obj get an object from pool.
    /// </summary>
    protected abstract void OnGetFromPool(T obj);

    /// <summary>
    /// Called when obj get's returned to pool.
    /// </summary>
    protected abstract void OnRelease(T obj);

    /// <summary>
    /// Called when no more capacity in pool.
    /// </summary>
    protected abstract void OnDestruction(T obj);
}
