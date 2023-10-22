using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Goal: Handles Player Inputs
/// </summary>

public class InputSystem : MonoBehaviour
{
    private GameManager gameManager;

    private PlayerInput inputs;

    private InputAction actionTouch0;
    private InputAction actionTouch1;
    private InputAction actionJump;
    private InputAction actionPointer;
    private InputAction actionMovement;
    private InputAction actionRotate;

    [SerializeField] private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;

    public Action OnMouseLeftClick;

    bool isHoldingLeftStick = false;
    bool isHoldingRightStick = false;


    //Cache
    private Vector3 movementAxis = Vector3.zero;
    public Vector3 MovementAxis => movementAxis;

    private Vector3 rotationAxis = Vector3.zero;
    public Vector3 RotationAxis => rotationAxis;

    // Movement stick
    [SerializeField] VirtualJoystick leftStick;

    //Rotation stick
    [SerializeField] VirtualJoystick rightStick;

    private void Awake()
    {
        inputs = GetComponent<PlayerInput>();
        eventSystem = GetComponent<EventSystem>();

        actionPointer = inputs.actions["Pointer"];
        actionTouch0 = inputs.actions["Touch0"];
        actionTouch1 = inputs.actions["Touch1"];
        //actionJump = inputs.actions["Jump"];

        actionMovement = inputs.actions["Movement"];
        actionRotate = inputs.actions["Rotate"];

    }

    private void OnEnable()
    {
        inputs.ActivateInput();

        actionPointer.Enable();
        //actionJump.Enable();
        actionTouch0.Enable();
        actionTouch1.Enable();

        //actionJump.performed += HandleJump;
        //actionPointer.performed += HandlePointer;
        actionTouch0.started += HandleMovementStarted;
        actionTouch1.started += HandleMovementStarted;

        actionTouch0.performed += HandleMovementStarted;
        actionTouch1.performed += HandleMovementStarted;
    }

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        if (!gameManager) Debug.LogError("Missing GameManager.", gameObject);
    }

    void Update()
    {
        var moveAxis = leftStick.GetAxis();
        movementAxis = new Vector3(moveAxis.x, 0, moveAxis.y);

        var rotAxis = rightStick.GetAxis();
        rotationAxis = new Vector3(rotAxis.x, 0, rotAxis.y); 
    }

    private void OnDisable()
    {
        actionPointer.Disable();
        actionTouch0.Disable();
        actionTouch1.Disable();
        //actionJump.Disable();

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
                isHoldingLeftStick = true;
            }
            else if(result.gameObject.name == "RightStickCenter")
            {
                rightStick.HandleMove(position);
                isHoldingRightStick = true;
            }
        }
    }

    private void HandleMovementPerformed(InputAction.CallbackContext context)
    {
        isHoldingRightStick = false;
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

    private void HandleJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
}
