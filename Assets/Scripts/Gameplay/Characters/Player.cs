
using UnityEngine;


// In this game, the player will be part of DDOL since not multiplayer game
public class Player : MonoBehaviour
{
    private int attack;

    private Health health;

    [SerializeField] InputSystem inputSystem;
    private CharacterController characterController;

    public float speed;

    [SerializeField] private float fallSpeed = 10.0f;

    private void Awake()
    {
        health = new Health(10);

        characterController = GetComponent<CharacterController>();  
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!characterController.isGrounded) 
        {
            characterController.Move(fallSpeed * Time.deltaTime * -transform.up);
            return;
        }

        characterController.Move(inputSystem.MovementAxis * Time.deltaTime * speed);
        if(inputSystem.RotationAxis != Vector3.zero) transform.forward = inputSystem.RotationAxis;
    }

    [ContextMenu("Fire()")]
    void Fire()
    {
        var pooledBullet = PlayerBulletPooler.Instance.Pool.Get();

        pooledBullet.SetPosition(transform.position);
        pooledBullet.SetDirection(transform.forward);
    }
}
