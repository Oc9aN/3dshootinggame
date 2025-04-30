using UnityEngine;

public class PlayerSpine : PlayerComponent
{
    // 맘에 안드는 애니메이션 보정
    [SerializeField]
    private float _YRifleAngle = 30f;

    [SerializeField]
    private float _YBatAngle = -30f;

    private float _YAngle = 0f;

    private Transform _spine; // 아바타의 상체
    private Animator _animator;

    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _animator = transform.GetComponentInChildren<Animator>();
        _spine = _animator.GetBoneTransform(HumanBodyBones.Spine); // 상체 transform 가져오기
        _camera = Camera.main;

        Player.OnCurrentWeaponChanged += OnWeaponChanged;
    }

    private void LateUpdate()
    {
        _spine.transform.localEulerAngles += new Vector3(_camera.transform.eulerAngles.x, _YAngle, _camera.transform.eulerAngles.z);
    }

    private void OnWeaponChanged(Weapon currentWeapon)
    {
        if (currentWeapon.Data.Type == EWeaponType.Rifle)
        {
            _YAngle = _YRifleAngle;
        }
        else if (currentWeapon.Data.Type == EWeaponType.Bat)
        {
            _YAngle = _YBatAngle;
        }
    }
}