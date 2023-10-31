using System.Collections;

using UnityEngine;

public class Enemy : MonoBehaviour, IActor
{
    [SerializeField] private SOActor so;

    private Health health;

    private SpriteRenderer rend;

    private bool isUnderCover = false;

    private Rigidbody2D rigid;

    private IActor target;

    private Coroutine fireCoroutine = null;

    [SerializeField] Transform rifle;
    [SerializeField] float firingAngle = 1.0f;

    [SerializeField] float delayBtwnShots = 3.0f;

    static LevelManager levelManager = null;

    private void Awake()
    {
        gameObject.name = so.Name;
        gameObject.tag = so.Tag;
        gameObject.layer = LayerMask.NameToLayer(so.Layer);

        health = GetComponent<Health>();
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = so.sprite;
        rend.color = so.color;

        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        health.SetPoints(so.HitPoints);
        rigid.velocity = -Vector2.up * so.Speed;
        isUnderCover = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Sandbag"))
        {
            isUnderCover = true;
            rigid.velocity = Vector2.zero;
        }
        else if(collision.CompareTag("Barbwire"))
        {
            rigid.velocity /= 2;
        }
    }

    private void Update()
    {
        if (isUnderCover)
        {
            target ??= Trench.player;

            Vector3 lookAtDir = target.GetTransform().position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-Vector3.forward, lookAtDir), 15 * Time.deltaTime);

            if(Vector3.Angle(transform.up, lookAtDir) <= 1.0f)
            {
                Fire();
            }
        }
    }

    public void TakeDamage(int damage, Vector3 at, Vector3 up)
    {
        if (isUnderCover) damage -= 1;

        health.TakeDamage(damage);

        SpawnBlood(at, up);

        if(!health.IsAlive())
        {
            if(!levelManager) levelManager = FindAnyObjectByType<LevelManager>();

            levelManager.IncreaseKillCount();
            
            fireCoroutine = null;

            StopAllCoroutines();

            FootSoldierEnemyPooler.Instance.Pool.Release(this);
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

    private IEnumerator FireRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(delayBtwnShots);

        while (health.IsAlive())
        {
            Shoot();
            yield return wait;
        }
    }
    private void Fire()
    {
        if (fireCoroutine != null) return;

        fireCoroutine = StartCoroutine(FireRoutine());
    }

    private void Shoot()
    {
        Vector2 aimCircle = Random.insideUnitCircle * firingAngle;

        Vector3 aimDirection = (Vector2)(target.GetTransform().position - transform.position);

        RaycastHit2D pointer = Physics2D.Raycast(rifle.position, (Vector2)aimDirection + aimCircle, aimDirection.magnitude + aimCircle.magnitude, LayerMask.GetMask("Allies"));
        //Debug.DrawRay(rifle.position, aimDirection + (Vector3)aimCircle, Color.red, 5.0f);
        // Check if hit anything
        Collider2D collider = pointer.collider;

/*        if(collider) Debug.Log(collider.name);
        else Debug.Log(" No Hit");*/

        var trail = ProjectileTrailPooler.Instance.Pool.Get();

        if (!collider)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= 0.8f) // 80% chance to shoot to shoot dirt
            {
                trail.SetPositions(rifle.position, target.GetTransform().position + (Vector3)aimCircle);
            }
            else // WildShot 20% chance
            {
                trail.SetWildShot(rifle.position, transform.up, aimCircle);
            }
        }
        else
        {
            if (collider.CompareTag("Ally"))
            {
                var ally = collider.GetComponent<Ally>();

                ally.TakeDamage(so.AttackPoints, pointer.point, transform.position);

                trail.SetPositions(rifle.position, pointer.point);
            }
            else if (collider.CompareTag("Player"))
            {
                var player = collider.GetComponent<Player>();

                player.TakeDamage(so.AttackPoints, pointer.point, transform.position);

                trail.SetPositions(rifle.position, pointer.point);
            }
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
