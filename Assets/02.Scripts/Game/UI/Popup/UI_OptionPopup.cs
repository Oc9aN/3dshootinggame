using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_OptionPopup : MonoBehaviour, IPopup
{
    [SerializeField]
    private Button _continueButton;
    
    [SerializeField]
    private Button _retryButton;
    
    [SerializeField]
    private Button _quitButton;
    
    [SerializeField]
    private Button _creditButton;
    
    [SerializeField]
    private UI_CreditPopup _creditPopup;
    
    private void Start()
    {
        _continueButton.onClick.AddListener(OnClickContinueButton);
        _retryButton.onClick.AddListener(OnClickRetryButton);
        _quitButton.onClick.AddListener(OnClickQuitButton);
        _creditButton.onClick.AddListener(OnClickCreditButton);
        
        GameManager.Instance.OptionPopup = this;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        PopupManager.Instance.Popups.Push(this);
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnClickContinueButton()
    {
        GameManager.Instance.Continue();
    }

    private void OnClickRetryButton()
    {
        GameManager.Instance.Restart();
    }

    private void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnClickCreditButton()
    {
        _creditPopup.Open();
    }
}