using System;
using UnityEngine;

public class CursorSetting : MonoBehaviour
{
    private void Start()
    {
        ViewManager.Instance.OnViewChanged += OnViewChanged;
        
        CursorActive(false);
    }

    private void Update()
    {
        if (InputHandler.GetKeyDown(KeyCode.Escape))
        {
            // UI 열기
            CursorActive(true);
        }
        else if (InputHandler.GetMouseButtonDown(0))
        {
            // UI 닫고 게임 재개
            CursorActive(false);
        }
    }

    private void CursorActive(bool active)
    {
        if (active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (ViewManager.Instance.ViewType == EViewType.QuarterView) // 쿼터뷰 마우스 커서는 항상 보여짐
            {
                Cursor.lockState = CursorLockMode.Confined;
                return;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnViewChanged(EViewType viewType)
    {
        if (viewType == EViewType.FirstPerson || viewType == EViewType.ThirdPerson)
        {
            CursorActive(false);
        }
        else
        {
            CursorActive(true);
        }
    }
}
