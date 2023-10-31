
using System;
using UnityEngine;

/// <summary>
/// Goal: Manages the overall game system.
/// </summary>

public class GameManager : MonoBehaviour
{
    // Note: Script MUST be attached to DDOL
    private static GameManager instance = null;
    public static GameManager GetInstance() => instance;

    /// The following are serizlized for debugging purposes
    [SerializeField] private SceneDirector sceneDirector = null;
    [SerializeField] private LevelManager levelManager = null;
    [SerializeField] private AudioManager audioManager = null;

    // Game Pause
    private enum GameState { PLAY, PAUSED, MAIN_MENU, CHAPTER1, CHAPTER2, CREDITS };
    private GameState currentGameState = GameState.MAIN_MENU;

    public Action OnGamePause;

    private void Awake()
    {
        if(!instance) instance = this;
        else Destroy(gameObject);

        sceneDirector = SceneDirector.GetInstance();
        if (!sceneDirector) Debug.LogError("Missing Scene Director.", gameObject);

        audioManager = AudioManager.Instance;
        if (!audioManager) Debug.LogError("Missing Audio Manager", gameObject);
    }

    private void OnEnable()
    {
        sceneDirector.OnSceneActivated += HandleLevelSceneActivation;
        sceneDirector.OnSceneActivated += HandleMainMenuSceneActivation;
    }

    public void HandleLevelSceneActivation(string sceneName)
    {
        if (!sceneName.Contains("Level")) return;

        currentGameState = GameState.PLAY;
    }

    public void HandleCreditsSceneActivation()
    {
        currentGameState = GameState.CREDITS;

    }

    public void HandleMainMenuSceneActivation(string sceneName)
    {
        if (!sceneName.Equals(SceneDirector.SceneNames.MAIN_MENU_SCENE)) return;

        currentGameState = GameState.MAIN_MENU;

        audioManager.Play(sceneName);
    }

    public void HandleGameOver()
    {

    }

    public void HandleGameWon()
    {

    }
}
