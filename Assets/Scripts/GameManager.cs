using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        CurrentState = GameState.Gameplay;
    }

    public void SetState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        Debug.Log($"Game State -> {newState}");

        OnGameStateChanged?.Invoke(newState);
    }

    public bool CanPlayerMove =>
        CurrentState == GameState.Gameplay;
}