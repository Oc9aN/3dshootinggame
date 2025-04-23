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
        
        if (Player.IsReloading)
        {
            Player.ReloadingProgress += Time.deltaTime;
            if (Player.ReloadingProgress >= Player.CurrentWeapon.Data.ReloadTime)
            {
                // 재장전
                Player.CurrentAmmo = Player.CurrentWeapon.Data.MaxAmmo;
                Player.IsReloading = false;
            }
        }
    }
}
