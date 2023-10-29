using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    [SerializeField] const float despawnTime = 5.0f;
    [SerializeField] float timeToDespawn = 0.0f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        BackToPooler();
    }

    private void Update()
    {
        timeToDespawn += Time.deltaTime;

        if (timeToDespawn >= despawnTime) BackToPooler(); 
    }

    public void SetDirection(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position= position;
    }

    private void BackToPooler()
    {
        //ProjectileTrailPooler.Instance.Pool.Release(this);
    }

    public void ResestTime()
    {
        timeToDespawn = 0.0f;
    }

    private Rigidbody rb;
}
