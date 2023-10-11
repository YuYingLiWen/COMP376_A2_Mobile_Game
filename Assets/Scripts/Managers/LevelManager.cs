using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Goal: Checks win & lose conditions.
/// </summary>

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float cameraScrollSpeed = 1.0f;

    private GameManager gameManager;
    private InputSystem inputSystem;

    public Action OnGameOver;
    public Action OnGameWon;

    //Cache
    private Camera cam;
    private Ray mouseRay;

    // gather 1000 stones


    private void Awake()
    {
        cam = Camera.main;

        gameManager = GameManager.GetInstance();
        if (!gameManager) Debug.LogError("Missing Game Manager", gameObject);

        inputSystem = gameManager.GetInputSystem();
    }

    private void OnEnable()
    {
        OnGameOver += gameManager.HandleGameOver;
        OnGameWon += gameManager.HandleGameWon;

        inputSystem.OnMouseLeftClick += HandleMouseLeftClick;
        inputSystem.OnMapScroll += HandleMapScroll;
    }

    private void OnDisable()
    {
        OnGameOver -= gameManager.HandleGameOver;
        OnGameWon -= gameManager.HandleGameWon;

        inputSystem.OnMouseLeftClick -= HandleMouseLeftClick;
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
    }

    private void GameWon()
    {
        OnGameWon?.Invoke();
    }

    private void HandleMapScroll(Vector2 axis)
    {
        cam.transform.Translate(axis * Time.deltaTime);
    }

    private void HandleMouseLeftClick()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawLine(mouseRay.origin, mouseRay.direction * 1000f, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, float.MaxValue))
        {
            var gameObject = hit.collider.gameObject;

            if (gameObject.CompareTag("Enemy")) // Example 
            {
                gameObject.GetComponent<TestEnemy>().Click();
            }
            else
            {
                Debug.Log(gameObject.name); // Example
            }
        }
    }
}
