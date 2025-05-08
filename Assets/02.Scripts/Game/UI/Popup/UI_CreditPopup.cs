using UnityEngine;

public class UI_CreditPopup : UI_Popup
{
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }
}
