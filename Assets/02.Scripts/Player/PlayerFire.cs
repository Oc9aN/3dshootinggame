using System;
using UnityEngine;

public class PlayerFire : PlayerComponent
{
    // 마우스 왼쪽과 오른쪽으로 총알과 수류탄을 발사

    [SerializeField]
    private GameObject _firePosition;

    [SerializeField]
    private ParticleSystem _bulletEffect;

    private float _fireRate = 0f;

    private float _bombForce = 1f;

    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    private void Update()
    {
        Fire();
        Bomb();
    }

    private void Fire()
    {
        _fireRate -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _fireRate <= 0 && Player.TryUseAmmo())
        {
            Ray ray = new Ray(_firePosition.transform.position, _camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _bulletEffect.transform.position = hit.point;
                _bulletEffect.transform.forward = hit.normal;
                _bulletEffect.Play();
            }
            // 허공에 쏘는 것도 쏘는 것
            _fireRate = Player.Data.FireRate;
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
        else if (Input.GetMouseButtonUp(1)
                 && Player.TryUseBomb())
        {
            Bomb bomb = Pool_Bomb.Instance.GetBomb();
            bomb.transform.position = _firePosition.transform.position;

            _bombForce = Mathf.Min(_bombForce, Player.Data.MaxBombForce);
            bomb.Fire(_bombForce);
        }
    }
}