using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwap : PlayerComponent
{
    // 플레이어가 사용하는 무기들
    // WeaponManager가 있어야할 듯
    [SerializeField]
    private Transform _weaponTransform;
    
    [SerializeField]
    private List<Weapon> _weaponPrefabList;

    private List<Weapon> _weaponList;

    private void Start()
    {
        _weaponList = new List<Weapon>(_weaponPrefabList.Count);
        
        foreach (var weaponPrefab in _weaponPrefabList)
        {
            Weapon weapon = Instantiate(weaponPrefab, _weaponTransform);
            weapon.gameObject.SetActive(false);
            _weaponList.Add(weapon);
        }
        SwapWeapons(0);
    }

    private void Update()
    {
        if (InputHandler.GetKeyDown(KeyCode.Alpha1))
        {
            // 무기에 맞는 애니메이션 트리거를 액션으로 지정
            SwapWeapons(0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Layer"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Mask"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Layer"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Mask"), 0);
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapons(1);
        }
        else if (InputHandler.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapons(2);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Layer"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Rifle Mask"), 0);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Layer"), 1);
            Player.Animator.SetLayerWeight(Player.Animator.GetLayerIndex("Melee Mask"), 1);
        }
    }

    private void SwapWeapons(int index)
    {
        Player.CurrentWeapon = _weaponList[index];
        Player.CurrentWeapon.AttackAnimationTrigger += () => Player.Animator.SetTrigger("Attack");
    }
}
