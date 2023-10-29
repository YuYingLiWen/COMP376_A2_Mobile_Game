
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
    [SerializeField] private GameInputSystem inputSystem = null;
    [SerializeField] private AudioManager audioManager = null;

    // Game Pause
    private enum GameState { PLAY, PAUSED, MAIN_MENU, CREDITS };
    private GameState currentGameState = GameState.MAIN_MENU;

    public Action OnGamePause;

    private void Awake()
    {
        if(!instance) instance = this;
        else Destroy(gameObject);

        sceneDirector = SceneDirector.GetInstance();
        if (!sceneDirector) Debug.LogError("Missing Scene Director.", gameObject);

    }

    private void OnEnable()
    {
        OnGamePause += HandleGamePause;
        sceneDirector.OnSceneActivated += HandleLevelSceneActivation;
        sceneDirector.OnSceneActivated += HandleMainMenuSceneActivation;

    }

    private void Start()
    {
        inputSystem = gameObject.GetComponentInChildren<GameInputSystem>();
        if (!inputSystem) Debug.LogError("Missing Input System", gameObject);

        audioManager = gameObject.GetComponentInChildren<AudioManager>();
        if (!audioManager) Debug.LogError("Missing Audio Manager", gameObject);
    }

    private void OnDisable()
    {
        OnGamePause -= HandleGamePause;
    }

    public void HandleLevelSceneActivation(string sceneName)
    {
        if (!sceneName.Contains("Level")) return;

        currentGameState = GameState.PLAY;

        inputSystem.enabled = true;
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

    private void HandleGamePause()
    {
        if      (currentGameState == GameState.PLAY)   currentGameState = GameState.PAUSED;
        else if (currentGameState == GameState.PAUSED) currentGameState = GameState.PLAY;


        if (currentGameState == GameState.PAUSED) // Show pause menu
        {

        }
        else // hide pause menu
        {

        }
    }

    public GameInputSystem GetInputSystem() => inputSystem;

    private const string LEVEL_MANAGER = "LevelManager";
}
