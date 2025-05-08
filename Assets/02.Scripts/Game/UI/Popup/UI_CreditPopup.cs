using System;
using UnityEngine;

public class UI_CreditPopup : MonoBehaviour, IPopup
{
    private void Awake()
    {
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
}
