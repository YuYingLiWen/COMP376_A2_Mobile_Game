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
    }

    private void OnDisable()
    {
        OnGameOver -= gameManager.HandleGameOver;
        OnGameWon -= gameManager.HandleGameWon;
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
}
