using UnityEngine;
using UnityEngine.UI;

public class UI_CreditPopup : UI_Popup
{
    [SerializeField]
    private Button _closeButton;

    protected override void Awake()
    {
        base.Awake();
        _closeButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickCloseButton()
    {
        PopupManager.Instance.ClosePopup(EPopupType.UI_CreditPopup);
    }
}
