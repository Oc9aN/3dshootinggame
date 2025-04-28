using System;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    private GameManger _gameModel;

    [SerializeField]
    private UI_Game _gameView;

    private void Awake()
    {
        _gameModel = GetComponent<GameManger>();
    }

    private void Start()
    {
        _gameModel.OnGameStateChanged += OnGameStateChanged;
        _gameView.OnReadyEnd += () => _gameModel.GameState = EGameState.Run;
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
