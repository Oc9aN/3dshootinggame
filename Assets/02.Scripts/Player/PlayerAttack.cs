using System;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : PlayerComponent
{
    // 마우스 왼쪽과 오른쪽으로 총알과 수류탄을 발사

    private float _fireRate = 0f;

    private float _bombForce = 1f;

    private Vector3 _currentRecoil;
    private Vector3 _targetRecoil;

    private void Update()
    {
        Fire();
        Bomb();
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            Player.CurrentWeapon.Attack();
        }
    }

    private void Bomb()
    {
        // 0: 왼쪽, 2: 오른쪽, 3: 휠
        if (Input.GetMouseButtonDown(1))
        {
            _bombForce = Player.Data.MinBombForce;
        }
        else if (Input.GetMouseButton(1))
        {
            // 차징
            _bombForce += Player.Data.AddBombForcePerSecond * Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Bomb bomb = Pool_Bomb.Instance.GetPooledObject();
            if (Player.BombCount <= 0 || ReferenceEquals(bomb, null))
            {
                return;
            }

            bomb.transform.position = transform.position;

            _bombForce = Mathf.Min(_bombForce, Player.Data.MaxBombForce);
            bomb.Fire(_bombForce);

            Player.BombCount--;
        }
    }
}