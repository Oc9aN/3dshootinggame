using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwap : PlayerComponent
{
    // TODO: 무기 데이터 관리 빼기
    [SerializeField]
    private List<SO_Weapon> _weaponData;

    private List<Weapon> _weapons;

    protected override void Awake()
    {
        base.Awake();
        _weapons = new List<Weapon>(_weaponData.Count);
        foreach (var data in _weaponData)
        {
            _weapons.Add(new Weapon(data));
        }
        SwapWeapons(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapons(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapons(1);
        }
    }

    private void SwapWeapons(int index)
    {
        Player.CurrentWeapon = _weapons[index];
    }
}
