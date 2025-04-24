using System;
using UnityEngine;

public class DefaultSetting : MonoBehaviour
{
    bool isUIOpen = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isUIOpen = !isUIOpen;

            if (isUIOpen)
            {
                // UI 열기
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // UI 닫고 게임 재개
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
