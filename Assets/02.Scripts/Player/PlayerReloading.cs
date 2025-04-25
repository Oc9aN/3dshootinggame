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
            Player.CurrentWeapon.IsReloading = true;
        }
        
        if (Player.CurrentWeapon.IsReloading)
        {
            Player.CurrentWeapon.ReloadingProgress += Time.deltaTime;
            if (Player.CurrentWeapon.ReloadingProgress >= Player.CurrentWeapon.Data.ReloadTime)
            {
                // 재장전
                Player.CurrentWeapon.CurrentAmmo = Player.CurrentWeapon.Data.MaxAmmo;
                Player.CurrentWeapon.IsReloading = false;
            }
        }
    }
}
