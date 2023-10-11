
using UnityEngine;
using UnityEngine.Pool;

public partial class GameObjectPooler : MonoBehaviour
{
    // Partial class containing Object1's implementations

    [SerializeField] private GameObject Obj1;

    GameObject OnCreateObj1()
    {
        return Instantiate(Obj1);
    }

    void OnGetObj1(GameObject obj)
    {
    }

    void OnReleaseObj1(GameObject obj)
    {
    }

    void OnDestroyObj1(GameObject obj)
    {
    }

    public ObjectPool<GameObject> GetPool1() => poolObject1;
}
