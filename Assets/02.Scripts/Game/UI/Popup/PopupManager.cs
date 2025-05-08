using System;
using System.Collections.Generic;

public class PopupManager : Singleton<PopupManager>
{
    // 팝업 뎁스를 관리
    private Stack<IPopup> _popups = new Stack<IPopup>();
    public Stack<IPopup> Popups => _popups;

    public void PopupClose(Action failCallback = null)
    {
        if (!_popups.TryPop(out IPopup topPopup))
        {
            failCallback?.Invoke();
            return;
        }
        topPopup.Close();
    }
}