using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Goal: Handles Player Inputs
/// </summary>
public class GameInputSystem : MonoBehaviour
{
    private static GameInputSystem instance;
    public static GameInputSystem Instance => instance;

    private GameManager gameManager;

    private PlayerInput inputs;

    private InputAction actionTouch0;
    private InputAction actionTouch1;
    private InputAction actionPointer;
    private InputAction actionMovement;
    private InputAction actionFire;

    public InputAction OnFire => actionFire;

    [SerializeField] private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;

    //Cache
    private Vector2 movementAxis = Vector3.zero;
    public Vector2 MovementAxis() {

        //Debug.Log("Input :"+movementAxis);
        return movementAxis;
    }
    // Fire Button
    [SerializeField] private Button fire;
    public Button Fire => fire;


    // Movement stick
    [SerializeField] VirtualJoystick leftStick;
    public VirtualJoystick AimJoystick => leftStick;

    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("InputSystem already exist in scene", this);

        inputs = GetComponent<PlayerInput>();
        eventSystem = GetComponent<EventSystem>();

        actionPointer = inputs.actions["Pointer"];
        actionTouch0 = inputs.actions["Touch0"];
        actionTouch1 = inputs.actions["Touch1"];
        actionFire = inputs.actions["Fire"];

        actionMovement = inputs.actions["Movement"];
    }

    private void OnEnable()
    {
        inputs.ActivateInput();

        actionPointer.Enable();
        actionTouch0.Enable();
        actionTouch1.Enable();
        actionFire.Enable();

        //actionPointer.performed += HandlePointer;
        actionTouch0.started += HandleMovementStarted;
        actionTouch1.started += HandleMovementStarted;

        actionTouch0.performed += HandleMovementStarted;
        actionTouch1.performed += HandleMovementStarted;
    }

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        if (!gameManager) Debug.LogWarning("Missing GameManager.", gameObject);
    }

    void Update()
    {
        movementAxis = leftStick.GetAxis();
    }

    private void OnDisable()
    {
        actionPointer.Disable();
        actionTouch0.Disable();
        actionTouch1.Disable();
        actionFire.Disable();

        inputs.DeactivateInput();
    }

    private void HandleMovementStarted(InputAction.CallbackContext context)
    {
        Vector2 position = context.ReadValue<Vector2>();
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = position;
        List<RaycastResult> raycastResult = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, raycastResult);

        foreach(RaycastResult result in raycastResult)
        {
            if(result.gameObject.name == "LeftStickCenter")
            {
                leftStick.HandleMove(position);
            }
        }
    }

    private void HandleMovementPerformed(InputAction.CallbackContext context)
    {

    }

    private void HandleTouch1(InputAction.CallbackContext context)
    {
        try
        {
            print("Touch1: " + context.ReadValue<Vector2>());
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void HandlePointer(InputAction.CallbackContext context)
    {
        try
        {
            print("Touch Pointer: " + context.ReadValue<Vector2>());
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
