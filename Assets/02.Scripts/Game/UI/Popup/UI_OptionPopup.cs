using UnityEngine;
using UnityEngine.UI;

public class UI_OptionPopup : UI_Popup
{
    [SerializeField]
    private Button _continueButton;
    
    [SerializeField]
    private Button _retryButton;
    
    [SerializeField]
    private Button _quitButton;
    
    [SerializeField]
    private Button _creditButton;

    protected override void Awake()
    {
        base.Awake();
        _continueButton.onClick.AddListener(OnClickContinueButton);
        _retryButton.onClick.AddListener(OnClickRetryButton);
        _quitButton.onClick.AddListener(OnClickQuitButton);
        _creditButton.onClick.AddListener(OnClickCreditButton);
    }

    private void Start()
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
        PopupManager.Instance.OpenPopup(EPopupType.UI_CreditPopup);
    }
}