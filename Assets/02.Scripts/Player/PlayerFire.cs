using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    private Vector3 _currentRecoil;
    private Vector3 _targetRecoil;

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
        if (Input.GetMouseButton(0) && _fireRate <= 0 && Player.CurrentAmmo > 0)
        {
            ApplyRandomRecoil(); // 먼저 반동 값을 계산

            // 반동 계산
            _currentRecoil = Vector3.Lerp(_currentRecoil, _targetRecoil, Time.deltaTime * Player.Data.RecoilSpeed);
            _targetRecoil = Vector3.Lerp(_targetRecoil, Vector3.zero, Time.deltaTime * Player.Data.RecoilReturnSpeed);

            _camera.transform.localEulerAngles += _currentRecoil; // 카메라 회전 적용

            Vector3 fireDirection = _camera.transform.rotation * Vector3.forward;
            Ray ray = new Ray(_firePosition.transform.position, fireDirection);
            Debug.DrawRay(_firePosition.transform.position, fireDirection * 100f, Color.blue, 100f);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _bulletEffect.transform.position = hit.point;
                _bulletEffect.transform.forward = hit.normal;
                _bulletEffect.Play();
            }
            // 허공에 쏘는 것도 쏘는 것
            _fireRate = Player.Data.FireRate;
            Player.CurrentAmmo--;
            // 재장전 중이면 중지
            if (Player.IsReloading)
            {
                Player.IsReloading = false;
            }
        }
        // 반동 계산 (발사하지 않을 때도 목표 반동 감소)
        else
        {
            _currentRecoil = Vector3.Lerp(_currentRecoil, _targetRecoil, Time.deltaTime * Player.Data.RecoilSpeed);
            _targetRecoil = Vector3.Lerp(_targetRecoil, Vector3.zero, Time.deltaTime * Player.Data.RecoilReturnSpeed);
            _camera.transform.localEulerAngles += _currentRecoil;
        }
    }

    private void ApplyRandomRecoil()
    {
        float vertical = Random.Range(0f, Player.Data.VerticalRecoil); // 위로 튕김
        float horizontal = Random.Range(-Player.Data.HorizontalRecoil, Player.Data.HorizontalRecoil);   // 좌우 랜덤
        _targetRecoil += new Vector3(vertical, horizontal, 0f);
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
            Bomb bomb = Pool_Bomb.Instance.GetBomb();
            if (Player.BombCount <= 0 || ReferenceEquals(bomb, null))
            {
                return;
            }
            bomb.transform.position = _firePosition.transform.position;

            _bombForce = Mathf.Min(_bombForce, Player.Data.MaxBombForce);
            bomb.Fire(_bombForce);

            Player.BombCount--;
        }
    }
}