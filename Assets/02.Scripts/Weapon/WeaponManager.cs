using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    // 전체 무기 관리
    // 무기 데이터
    public event Action<Weapon> OnWeaponChanged; 
    
    [SerializeField]
    private List<Weapon> _weaponPrefabList;

    private List<Weapon> _weaponList;

    protected override void InternalAwake()
    {
        base.InternalAwake();
        _weaponList = new List<Weapon>(_weaponPrefabList.Count);
        
        foreach (var weaponPrefab in _weaponPrefabList)
        {
            Weapon weapon = Instantiate(weaponPrefab, transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.gameObject.SetActive(false);
            _weaponList.Add(weapon);
        }
    }
    
    // 무기 가져가기
    public Weapon GetWeapon(EWeaponType type, Weapon returnWeapon, Transform parent)
    {
        if (!ReferenceEquals(returnWeapon, null))
        {
            ReturnWeapon(returnWeapon);
        }
        
        Weapon getWeapon = _weaponList[(int)type];
        
        getWeapon.gameObject.SetActive(true);
        getWeapon.transform.parent = parent;
        getWeapon.transform.localPosition = Vector3.zero;
        getWeapon.transform.localRotation = Quaternion.identity;
        getWeapon.InitializeWeapon();
        
        OnWeaponChanged?.Invoke(getWeapon);
        return getWeapon;
    }
    
    private void ReturnWeapon(Weapon returnWeapon)
    {
        returnWeapon.InitializeEvent();
        gameObject.SetActive(false);
        returnWeapon.transform.localPosition = Vector3.zero;
        returnWeapon.transform.localRotation = Quaternion.identity;
    }
}