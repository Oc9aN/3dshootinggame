using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    // 여러 팝업을 관리
    private List<UI_Popup> _popups;
    // 팝업 뎁스를 관리
    private List<UI_Popup> _openedPopups;

    protected override void InternalAwake()
    {
        base.InternalAwake();
        _popups = new List<UI_Popup>();
        _openedPopups = new List<UI_Popup>();
    }

    public void AddPopup(UI_Popup popup)
    {
        _popups.Add(popup);
    }
    
    public void OpenPopup(EPopupType popupType, Action closeCallback = null)
    {
        OpenPopup(popupType.ToString(), closeCallback);
    }
    
    private void OpenPopup(string popupName, Action closeCallback)
    {
        foreach (UI_Popup popup in _popups)
        {
            if (popup.name == popupName)
            {
                popup.Open(closeCallback);
                if (!_openedPopups.Exists(x => x.name == popupName))
                {
                    _openedPopups.Add(popup);
                }
            }
        }
    }

    public void ClosePopup(EPopupType popupType)
    {
        ClosePopup(popupType.ToString());
    }
    
    private void ClosePopup(string popupName)
    {
        foreach (UI_Popup popup in _popups)
        {
            if (popup.name == popupName)
            {
                popup.Close();
                _openedPopups.Remove(popup);
            }
        }
    }

    public void HighestPopupClose(Action failCallback = null)
    {
        Debug.Log(_openedPopups.Count);
        if (_openedPopups.Count <= 0)
        {
            failCallback?.Invoke();
            return;
        }
        _openedPopups[_openedPopups.Count - 1].Close();
        _openedPopups.RemoveAt(_openedPopups.Count - 1);
    }
}