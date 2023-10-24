using UnityEngine;

public class Player : MonoBehaviour
{
    private int attack;

    private Health health;

    [SerializeField] InputSystem inputSystem;

    [SerializeField] Transform aimPoint;
    [SerializeField] Transform debugAimPoint;

    [SerializeField] float firingAngle = 15.0f;
    [SerializeField] float aimSpeed = 3.0f;
    CapsuleCollider coll;

    private void Awake()
    {
        health = new Health(10);

        coll = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        inputSystem.AimJoystick.OnHoldingStart += HandleHoldingStart;
        inputSystem.AimJoystick.OnHoldingStop += HandleHoldingStop;

        inputSystem.Fire.onClick.AddListener(Fire);
    }

    // Update is called once per frame
    void Update()
    {
        if (HasCover) return;// No Cover
         
        Vector2 direction = inputSystem.MovementAxis;

        aimPoint.Translate(
            direction.x * aimSpeed * Time.deltaTime,
            0.0f,
            direction.y * aimSpeed * Time.deltaTime, Space.World);

        transform.forward = aimPoint.position - transform.position;
    }

    private void OnDisable()
    {
        inputSystem.Fire.onClick.RemoveListener(Fire);

        inputSystem.AimJoystick.OnHoldingStart -= HandleHoldingStart;
        inputSystem.AimJoystick.OnHoldingStop -= HandleHoldingStop;
    }

    [ContextMenu("Fire()")]
    void Fire()
    {
        float aimDistance = (aimPoint.position - transform.position).magnitude;

        Vector3 aimCircle = Random.onUnitSphere * firingAngle;
        aimCircle.y = transform.position.y; // Makes it only shoot forward.

        Debug.DrawRay(transform.position, transform.forward * aimDistance + aimCircle, Color.red, 1.0f);

        if (Physics.Raycast(transform.position, transform.forward * aimDistance + aimCircle, out RaycastHit hit, aimDistance)) //TODO: Check Layer Mask
        {
            Collider hitColl = hit.collider;
            debugAimPoint.position = hit.point;
            if (hitColl.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
            }
            else if(hitColl.CompareTag("Level"))
            {
                Debug.Log("Hit Level");
            }
        }
    }

    private void HandleHoldingStop()
    {
        aimPoint.gameObject.SetActive(false);
        aimPoint.position = transform.position;

        Cover();
    }

    private void HandleHoldingStart()
    {
        aimPoint.gameObject.SetActive(true);
        aimPoint.position = transform.position;

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

    bool HasCover => !coll.enabled;
}
