using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwap : PlayerComponent
{
    // 플레이어가 사용하는 무기들
    [SerializeField]
    private Transform _weaponTransform;

    private int _currentWeaponIndex = 0;

    private void Update()
    {
        if (InputHandler.GetKeyDown(KeyCode.Alpha1) || _currentWeaponIndex == 0)
        {
            SetCurrentWeaponIndex(1);
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurrentWeaponIndex(2);
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha3))
        {
            SetCurrentWeaponIndex(3);
        }
        
        // 마우스 휠
        float scroll = InputHandler.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            SetCurrentWeaponIndex(Mathf.Min(_currentWeaponIndex + 1, 3));
        }
        else if (scroll < 0f)
        {
            SetCurrentWeaponIndex(Mathf.Max(_currentWeaponIndex - 1, 1));
        }
    }

    private void SetCurrentWeaponIndex(int index)
    {
        _currentWeaponIndex = index;
        SwapHandler(_currentWeaponIndex);
    }

    private void SwapHandler(int index)
    {
        if (index == 1)
        {
            // 무기에 맞는 애니메이션 트리거를 액션으로 지정
            SwapWeapons(EWeaponType.Rifle);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Layer"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Mask"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Layer"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Mask"), 0);
        }
        else if (index == 2)
        {
            SwapWeapons(EWeaponType.Bat);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Layer"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Mask"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Layer"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Mask"), 1);
        }
        else if (index == 3)
        {
            SwapWeapons(EWeaponType.Pistol);
        }
    }

    private void SwapWeapons(EWeaponType type)
    {
        Weapon changedWeapon = WeaponManager.Instance.GetWeapon(type, Player.CurrentWeapon, _weaponTransform);
        changedWeapon.AttackAnimationTrigger += () => Player.Animator.SetTrigger("Attack");
        Player.CurrentWeapon = changedWeapon;
    }
}