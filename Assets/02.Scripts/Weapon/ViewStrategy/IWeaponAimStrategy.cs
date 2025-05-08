using UnityEngine;

public interface IWeaponAimStrategy
{
    // 시야에 따른 공격 방식
    /// <summary>
    /// 시야에 따른 공격 방향을 계산
    /// </summary>
    /// <param name="weapon">현재 무기</param>
    /// <returns></returns>
    public Vector3 GetWeaponAimingDirection(Weapon weapon);
}