using UnityEngine;

[CreateAssetMenu(fileName = "SO_Weapon", menuName = "Scriptable Objects/SO_Weapon")]
public class SO_Weapon : ScriptableObject
{
    [Header("Type")]
    [SerializeField]
    private EWeaponType _type;
    public EWeaponType Type => _type;
    
    [Header("Fire")]
    [SerializeField]
    private int _maxAmmo = 50;
    public int MaxAmmo => _maxAmmo;

    [SerializeField]
    private float _fireRate = 0.5f;
    public float FireRate => _fireRate;
    
    [SerializeField]
    private float _reloadTime = 2f;
    public float ReloadTime => _reloadTime;
    
    [Header("Recoil")]
    [SerializeField]
    private float _verticalRecoil = 2f;
    public float VerticalRecoil => _verticalRecoil;
    
    [SerializeField]
    private float _horizontalRecoil = 1f;
    public float HorizontalRecoil => _horizontalRecoil;
    
    [SerializeField]
    private float _recoilSpeed = 10f;
    public float RecoilSpeed => _recoilSpeed;
    
    [SerializeField]
    private float _recoilReturnSpeed = 20f;
    public float RecoilReturnSpeed => _recoilReturnSpeed;
    
    [Header("Bullet")]
    [SerializeField]
    private float _bulletMaxDistance = 100f;
    public float BulletMaxDistance => _bulletMaxDistance;
    
    [SerializeField]
    private float _bulletSpeed = 100f;
    public float BulletSpeed => _bulletSpeed;

    [SerializeField]
    private Damage _damage;
    public Damage Damage => _damage;
    
    [SerializeField]
    private Sprite _weaponSprite;
    public Sprite WeaponSprite => _weaponSprite;
}
