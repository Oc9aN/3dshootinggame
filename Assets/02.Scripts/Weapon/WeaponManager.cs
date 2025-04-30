using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    // 전체 무기 관리
    // 무기 데이터
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
            weapon.Initialize();
            _weaponList.Add(weapon);
        }
    }
    // 무기 가져가기
    public Weapon GetWeapon(EWeaponType type, Weapon returnWeapon)
    {
        if (!ReferenceEquals(returnWeapon, null))
        {
            ReturnWeapon(returnWeapon);
        }
        
        Weapon getWeapon = _weaponList[(int)type];
        getWeapon.gameObject.SetActive(true);
        return getWeapon;
    }
    
    // 무기 반환
    private void ReturnWeapon(Weapon returnWeapon)
    {
        returnWeapon.gameObject.SetActive(false);
        returnWeapon.transform.parent = transform;
        returnWeapon.transform.localPosition = Vector3.zero;
    }
}