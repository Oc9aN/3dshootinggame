using System;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private UI_Game _gameView;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        _gameView.OnReadyEnd += () => GameManager.Instance.GameState = EGameState.Run;
    }

    private void OnGameStateChanged(EGameState gameState)
    {
        switch (gameState)
        {
            case EGameState.Ready:
            {
                _gameView.Ready();
                break;
            }
            case EGameState.Run:
            {
                _gameView.Run();
                break;
            }
            case EGameState.Over:
            {
                _gameView.Over();
                break;
            }
        }
    }
}
