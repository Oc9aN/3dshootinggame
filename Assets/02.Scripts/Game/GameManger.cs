using System;
using System.Collections;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    // 게임 진행을 관리
    // Ready -> Run -> Over
    public event Action<EGameState> OnGameStateChanged;
    
    private EGameState _gameState = EGameState.Ready;

    public EGameState GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            if (_gameState == EGameState.Ready || _gameState == EGameState.Over)
            {
                InputHandler.BlockInput = true;
            }
            else
            {
                InputHandler.BlockInput = false;
            }
            OnGameStateChanged?.Invoke(_gameState);
        }
    }

    [SerializeField]
    private float _readyTime = 3f;
    
    private IEnumerator _readyCoroutine;

    private void Start()
    {
        GameState = EGameState.Ready;
    }

    public void GameOver()
    {
        GameState = EGameState.Over;
    }
}
