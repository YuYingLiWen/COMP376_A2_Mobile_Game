
using UnityEngine;


// In this game, the player will be part of DDOL since not multiplayer game
public class Player : MonoBehaviour
{
    private int attack;

    private Health health;

    [SerializeField] InputSystem inputSystem;
    private CharacterController characterController;

    public float speed;

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
        characterController.Move(inputSystem.MovementAxis * Time.deltaTime * speed);
        transform.forward = inputSystem.RotationAxis;
    }
}
