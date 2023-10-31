using System;
using UnityEngine;

/// <summary>
/// Goal: Checks win & lose conditions.
/// </summary>

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private FootSoldierEnemySpawner spawner;

    public Action OnGameOver;
    public Action OnGameWon;

    /// Objectives
    private const int killCount = 40;
    private int currentCount = 0;


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

    private void GameWon()
    {
        OnGameWon?.Invoke();
        GameManager.GetInstance().HasWon = true;
        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.CREDITS_SCENE, true);
    }
    public void GameOver()
    {
        OnGameOver?.Invoke();
        GameManager.GetInstance().HasWon = false;
        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.CREDITS_SCENE, true);
    }

    public void IncreaseKillCount()
    {
        currentCount += 1;

        if (currentCount % 10 == 0) spawner.IncreaseEnemyCountPerWave();

        if (currentCount == killCount) GameWon();
    }
}
