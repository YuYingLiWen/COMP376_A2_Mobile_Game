
using UnityEngine;

public class Ally : MonoBehaviour, IActor
{
    [SerializeField] private SOActor so;

    private Health health;

    private SpriteRenderer rend;

    private bool isUnderCover = false;

    private Rigidbody2D rigid;

    private void Awake()
    {
        gameObject.name = so.Name;
        gameObject.tag = so.Tag;
        gameObject.layer = LayerMask.NameToLayer(so.Layer);

        rend = GetComponent<SpriteRenderer>();
        rend.sprite = so.sprite;
        rend.color = so.color;
    }

    private void OnEnable()
    {
        health.SetPoints(so.HitPoints);
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isUnderCover = true;
        rigid.velocity = Vector2.zero;
    }

    private void OnDisable()
    {
    }

    public void TakeDamage(int damage, Vector3 at, Vector3 up)
    {
        health.TakeDamage(damage);

        SpawnBlood(at, up);

        if (!health.IsAlive())
        {
            FootSoldierAllyPooler.Instance.Pool.Release(this);
        }
    }

    public void SpawnBlood(Vector3 at, Vector3 up)
    {
        GameObject blood = BloodPooler.Instance.Pool.Get();
        blood.transform.up = at - up;
        blood.transform.position = at;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = (Vector2)pos;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}