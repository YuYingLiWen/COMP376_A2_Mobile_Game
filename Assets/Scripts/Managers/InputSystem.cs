using System;
using UnityEngine;


/// <summary>
/// Goal: Handles Player Inputs
/// </summary>

public class InputSystem : MonoBehaviour
{
    private GameManager gameManager;



    public Action OnMouseLeftClick;

    //Cache
    private Vector2 axis = Vector2.zero; 

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        if (!gameManager) Debug.LogError("Missing GameManager.", gameObject);
    }

    void Update()
    {
        KeyboardEvents();
        MouseEvents();
    }

    private void KeyboardEvents()
    {
        // To Pause the game
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) gameManager.OnGamePause?.Invoke();
    }

    private void MouseEvents()
    {
        //Left click
        if (Input.GetKeyUp(KeyCode.Mouse0)) OnMouseLeftClick?.Invoke();
    }

    private void LateUpdate()
    {
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");
    }

}
