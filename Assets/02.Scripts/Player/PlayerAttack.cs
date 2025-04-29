using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : PlayerComponent
{
    // 마우스 왼쪽과 오른쪽으로 총알과 수류탄을 발사
    [SerializeField]
    private Transform _bombPosition;

    private float _fireRate = 0f;

    private float _bombForce = 1f;

    private Vector3 _currentRecoil;
    private Vector3 _targetRecoil;

    private IEnumerator _bombCoroutine;

    private void Update()
    {
        Attack();
        Bomb();
    }

    private void Attack()
    {
        if (InputHandler.GetMouseButton(0))
        {
            Player.CurrentWeapon.Attack();
        }
    }

    private void Bomb()
    {
        // 0: 왼쪽, 2: 오른쪽, 3: 휠
        if (InputHandler.GetMouseButtonDown(1))
        {
            _bombForce = Player.Data.MinBombForce;
        }
        else if (InputHandler.GetMouseButton(1))
        {
            // 차징
            _bombForce += Player.Data.AddBombForcePerSecond * Time.deltaTime;
        }
        else if (InputHandler.GetMouseButtonUp(1))
        {
            Player.Animator.SetTrigger("Throw");
            _bombCoroutine = Bomb_Coroutine();
            StartCoroutine(_bombCoroutine);
        }
    }

    private IEnumerator Bomb_Coroutine()
    {
        yield return new WaitForSeconds(0.7f);
        BombFire();
    }

    private void BombFire()
    {
        Bomb bomb = Pool_Bomb.Instance.GetPooledObject();
        if (Player.BombCount <= 0 || ReferenceEquals(bomb, null))
        {
            return;
        }

        bomb.transform.position = _bombPosition.position;

        _bombForce = Mathf.Min(_bombForce, Player.Data.MaxBombForce);
        Debug.Log("Boom");
        bomb.Fire(_bombForce);

        Player.BombCount--;
    }
}