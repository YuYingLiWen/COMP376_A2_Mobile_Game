using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IActor
{
    private int attackPoints; // Damage per shot fired

    private Health health;

    [SerializeField] private GameInputSystem inputSystem;

    [SerializeField] private Transform rifle;
    [SerializeField] private AimPoint aimPoint;

    [SerializeField] private float firingAngle = 15.0f;
    [SerializeField] private float aimSpeed = 3.0f;
    private CircleCollider2D coll;

    private void Awake()
    {
        health = new Health(10);

        coll = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        inputSystem.AimJoystick.OnHoldingStart += HandleHoldingStart;
        inputSystem.AimJoystick.OnHoldingStop += HandleHoldingStop;
        inputSystem.Fire.onClick.AddListener(Fire);
    }

    private void Start()
    {
        inputSystem.OnFire.performed += HandleFire;
    }

    private void OnDisable()
    {
        inputSystem.Fire.onClick.RemoveListener(Fire);

        inputSystem.AimJoystick.OnHoldingStart -= HandleHoldingStart;
        inputSystem.AimJoystick.OnHoldingStop -= HandleHoldingStop;
    }

    void Update()
    {
        if (HasCover) return;// No Cover

        aimPoint.Translate(inputSystem.MovementAxis(), in aimSpeed);

        transform.up = (Vector3)aimPoint.WorldPoint - transform.position;
    }

    void HandleFire(InputAction.CallbackContext context)
    {
        Fire();
    }

    [ContextMenu("Fire()")]
    void Fire()
    {
        Vector2 aimCircle = Random.insideUnitCircle * firingAngle;

        // Check if distance is too close to player 
        float aimDistance = (aimCircle + aimPoint.WorldPoint - (Vector2)transform.position).magnitude;
        if (aimDistance <= 1.0f) return; // Too close to self

        RaycastHit2D pointer = aimPoint.Raycast(transform.position, aimCircle);  //TODO: Check Layer Mask

        // Check if hit anything
        Collider2D collider = pointer.collider;

        var trail = ProjectileTrailPooler.Instance.Pool.Get();

        if (!collider)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= 0.8f) // 80% chance to shoot to shoot dirt
            {
                trail.SetPositions(rifle.position, aimPoint.WorldPoint + aimCircle);
            }
            else // WildShot 20% chance
            {
                trail.SetWildShot(rifle.position, transform.up, aimCircle);
            }
        }
        else
        {
            if (collider.CompareTag("Enemy"))
            {
                var enemy = collider.GetComponent<Enemy>();

                enemy.SpawnBlood(pointer.point, transform.position);
                enemy.TakeDamage(attackPoints);

                trail.SetPositions(rifle.position, pointer.point);
            }
        }
    }

    private void HandleHoldingStop()
    {
        aimPoint.gameObject.SetActive(false);
        aimPoint.SetRectPosition(transform.position);

        Cover();
    }

    private void HandleHoldingStart()
    {
        aimPoint.gameObject.SetActive(true);
        aimPoint.SetRectPosition(transform.position);

        Uncover();
    }

    // Player is behind some cover to mitigate damage
    void Cover()
    {
        coll.enabled = false;
    }

    // Player is in the open; Player takes full damage
    void Uncover()
    {
        coll.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public void SpawnBlood(Vector3 at, Vector3 up)
    {

    }

    bool HasCover => !coll.enabled;
}
