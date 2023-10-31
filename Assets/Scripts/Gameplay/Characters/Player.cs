using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IActor
{
    [SerializeField] private int healthPoint = 10;
    [SerializeField] private int attackPoints = 5; // Damage per shot fired

    [SerializeField] private Health health;

    [SerializeField] private GameInputSystem inputSystem;

    [SerializeField] private Transform rifle;
    [SerializeField] private AimPoint aimPoint;

    [SerializeField] private float firingAngle = 0.5f;
    [SerializeField] private float aimSpeed = 3.0f;
    private CircleCollider2D coll;

    [SerializeField] private Reload reload;

    [SerializeField] private Image hpBar;

    private AudioSource fireSFX;

    private void Awake()
    {
        health = GetComponent<Health>();

        coll = GetComponent<CircleCollider2D>();

        fireSFX = GetComponent<AudioSource>();
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


        //Add modifiers
        PlayerDataModifier mods = GameManager.GetInstance().playerModifier;

        health.SetPoints(healthPoint + mods.hp);
        attackPoints += mods.attack;
        firingAngle += ((float)mods.spread / 2.0f);
        reload.ReloadTime -= mods.speed;
    }

    private void OnDisable()
    {
        inputSystem.Fire.onClick.RemoveListener(Fire);

        inputSystem.AimJoystick.OnHoldingStart -= HandleHoldingStart;
        inputSystem.AimJoystick.OnHoldingStop -= HandleHoldingStop;
    }

    void Update()
    {
        UpdateCover();
    }



    void HandleFire(InputAction.CallbackContext context)
    {
        Fire();
    }

    [ContextMenu("Fire()")]
    void Fire()
    {
        if (!reload.CanFire) return;

        fireSFX.Play();
        reload.OnFire();// UI image bullet;

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

                enemy.TakeDamage(attackPoints, pointer.point, transform.position);

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

    private void UpdateCover()
    {
        if (HasCover) return;

        aimPoint.Translate(inputSystem.MovementAxis(), in aimSpeed);

        transform.up = (Vector3)aimPoint.WorldPoint - transform.position;
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

    [ContextMenu("Take DAmage")]
    void debugTAke()
    {
        health.TakeDamage(1);
        Debug.Log(health.GetPoints());
        hpBar.fillAmount = health.GetHealthPercent();

    }

    public void TakeDamage(int damage, Vector3 at, Vector3 up)
    {
        health.TakeDamage(damage);

        hpBar.fillAmount = health.GetHealthPercent();

        if (!health.IsAlive()) FindAnyObjectByType<LevelManager>().GameOver();
    }

    public void SpawnBlood(Vector3 at, Vector3 up)
    {
        GameObject blood = BloodPooler.Instance.Pool.Get();
        blood.transform.up = at - up;
        blood.transform.position = at;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    bool HasCover => !coll.enabled;
}
