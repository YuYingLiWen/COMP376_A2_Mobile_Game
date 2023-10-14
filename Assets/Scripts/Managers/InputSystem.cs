using System;
using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

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

    [SerializeField] private TextMeshProUGUI debugGUI;

    public Action OnMouseLeftClick;

    //Cache
    private Vector2 axis = Vector2.zero;

    private void Awake()
    {
        inputs = GetComponent<PlayerInput>();

        actionPointer = inputs.actions["Pointer"];
        actionTouch0 = inputs.actions["Touch0"];
        actionTouch1 = inputs.actions["Touch1"];
        actionJump = inputs.actions["Jump"];

    }

    private void OnEnable()
    {
        inputs.ActivateInput();

        actionPointer.Enable();
        actionJump.Enable();
        actionTouch0.Enable();
        actionTouch1.Enable();

        actionJump.performed += HandleJump;
        actionPointer.performed += HandlePointer;
        actionTouch0.performed += HandleTouch0;
        actionTouch1.performed += HandleTouch1;
    }

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        if (!gameManager) Debug.LogError("Missing GameManager.", gameObject);
    }

    void Update()
    {

    }

    private void OnDisable()
    {
        actionPointer.Disable();
        actionTouch0.Disable();
        actionTouch1.Disable();
        actionJump.Disable();

        inputs.DeactivateInput();
    }

    private void HandleTouch0(InputAction.CallbackContext context)
    {
        try
        {
            print(context.valueType);
            print("Touch0: " + context.ReadValue<Vector2>());
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
       // if (debugGUI.text.Length > 1000) debugGUI.text = "";

        //debugGUI.text += "Touch0: " + context.ReadValue<Vector2>() + "\n";
    }

    private void HandleTouch1(InputAction.CallbackContext context)
    {
        try
        {
            print(context.valueType);
            print("Touch1: " + context.ReadValue<Vector2>());
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
       // if (debugGUI.text.Length > 1000) debugGUI.text = "";

        //debugGUI.text += "Touch1: " + context.ReadValue<Vector2>() + "\n";
    }

    private void HandlePointer(InputAction.CallbackContext context)
    {
        try
        {
            print(context.valueType);
            print("Touch Pointer: " + context.ReadValue<Vector2>());
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //if (debugGUI.text.Length > 1000) debugGUI.text = "";
       // debugGUI.text += "Pointer: " + context.ReadValue<Vector2>() + "\n";

    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
}
