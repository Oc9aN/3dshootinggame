using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
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

    private void Update()
    {
        if (InputHandler.GetKeyDown(KeyCode.Escape))
        {
            PopupManager.Instance.HighestPopupClose(failCallback: Pause);
        }
    }

    private void Pause()
    {
        GameState = EGameState.Puase;
        Time.timeScale = 0;

        PopupManager.Instance.OpenPopup(EPopupType.UI_OptionPopup, closeCallback: Continue);
    }

    public void Continue()
    {
        GameState = EGameState.Run;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        GameState = EGameState.Run;
        Time.timeScale = 1;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
