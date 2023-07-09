using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    CountDown,
    Running,
    RaceOver
};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private GameStates _gameState = GameStates.CountDown;

    private float _raceStartedTime = 0;
    private float _raceCompletedTime = 0;

    public event Action<GameManager> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LevelStart()
    {
        _gameState = GameStates.CountDown;
    }

    public GameStates GetGameState()
    {
        return _gameState;
    }

    private void ChangeGameState(GameStates newGameState)
    {
        if (_gameState != newGameState)
        {
            _gameState = newGameState;

            OnGameStateChanged?.Invoke(this);
        }
    }

    public float GetRaceTime()
    {
        if (_gameState == GameStates.CountDown)
        {
            return 0;
        }
        else if (_gameState == GameStates.RaceOver)
        {
            return _raceCompletedTime - _raceStartedTime;
        }
        else
        {
            return Time.time - _raceStartedTime;
        }
    }

    public void OnRaceStart()
    {
        _raceStartedTime = Time.time;

        ChangeGameState(GameStates.Running);
    }

    public void OnRaceCompleted()
    {
        _raceCompletedTime = Time.time;

        ChangeGameState(GameStates.RaceOver);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }
}