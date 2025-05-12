using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoText;

    [SerializeField]
    private Image _weaponImage;
    
    [SerializeField]
    private Image _crosshairImage;

    public void RefreshAmmoText(int bulletCount, int maxBulletCount)
    {
        _ammoText.text = $"남은 총알 {bulletCount}/{maxBulletCount}";
    }

    public void RefreshWeaponImage(Sprite sprite)
    {
        _weaponImage.sprite = sprite;
    }

    public void RefreshCrosshairImage(Sprite sprite)
    {
        _crosshairImage.sprite = sprite;
    }

    public void ActiveCrosshair(bool active)
    {
        _crosshairImage.gameObject.SetActive(active);
    }
}
