using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwap : PlayerComponent
{
    // 플레이어가 사용하는 무기들
    [SerializeField]
    private Transform _weaponTransform;

    private int _currentWeaponIndex = 1;

    private int CurrentWeaponIndex
    {
        get => _currentWeaponIndex;
        set
        {
            _currentWeaponIndex = value;
            SwapHandler(_currentWeaponIndex);
        }
    }

    private void Start()
    {
        CurrentWeaponIndex = 1;
    }

    private void Update()
    {
        if (InputHandler.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeaponIndex = 1;
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeaponIndex = 2;
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentWeaponIndex = 3;
        }
        
        // 마우스 휠
        float scroll = InputHandler.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            CurrentWeaponIndex = Mathf.Min(CurrentWeaponIndex + 1, 3);
        }
        else if (scroll < 0f)
        {
            CurrentWeaponIndex = Mathf.Max(CurrentWeaponIndex - 1, 1);
        }
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
        Player.CurrentWeapon = WeaponManager.Instance.GetWeapon(type, Player.CurrentWeapon);
        Player.CurrentWeapon.transform.parent = _weaponTransform;
        Player.CurrentWeapon.transform.localPosition = Vector3.zero;
        Player.CurrentWeapon.transform.localRotation = Quaternion.identity;
        Player.CurrentWeapon.AttackAnimationTrigger += () => Player.Animator.SetTrigger("Attack");
    }
}