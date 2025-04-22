using System;
using UnityEngine;

public class PlayerReloading : PlayerComponent
{
    private void Update()
    {
        Reload();
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Player.IsReloading = true;
        }
    }
}
