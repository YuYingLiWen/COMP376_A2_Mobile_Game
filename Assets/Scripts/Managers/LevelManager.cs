using System;
using UnityEngine;

/// <summary>
/// Goal: Checks win & lose conditions.
/// </summary>

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;

    public Action OnGameOver;
    public Action OnGameWon;

    /// Objectives
    private const int killCount = 50;


    bool debugMode = false;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        if (!gameManager)
        {
            Debug.LogError("Missing Game Manager", gameObject);
            debugMode = true;
        }
    }

    private void OnEnable()
    {
        if (debugMode) return;
        OnGameOver += gameManager.HandleGameOver;
        OnGameWon += gameManager.HandleGameWon;
    }

    private void OnDisable()
    {
        if (debugMode) return;

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
}
