using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwap : PlayerComponent
{
    // 플레이어가 사용하는 무기들
    [SerializeField]
    private Transform _weaponTransform;

    private void Start()
    {
        SwapWeapons(0);
    }

    private void Update()
    {
        if (InputHandler.GetKeyDown(KeyCode.Alpha1))
        {
            // 무기에 맞는 애니메이션 트리거를 액션으로 지정
            SwapWeapons(EWeaponType.Rifle);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Layer"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Mask"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Layer"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Mask"), 0);
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapons(EWeaponType.Pistol);
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapons(EWeaponType.Bat);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Layer"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Mask"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Layer"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Mask"), 1);
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