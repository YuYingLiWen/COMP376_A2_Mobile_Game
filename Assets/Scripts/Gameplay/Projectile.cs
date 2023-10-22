using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerBulletPooler.Instance.Pool.Release(this);
    }

    public void SetDirection(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position= position;
    }

    private Rigidbody rb;
}
