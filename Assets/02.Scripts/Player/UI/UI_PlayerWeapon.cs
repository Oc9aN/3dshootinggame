using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoText;

    [SerializeField]
    private Image _weaponImage;

    public void Initialize(int maxBombCount)
    {
        _ammoText.text = $"남은 총알 {maxBombCount}/{maxBombCount}";
    }

    public void RefreshAmmoText(int bombCount, int maxBombCount)
    {
        _ammoText.text = $"남은 총알 {bombCount}/{maxBombCount}";
    }

    public void RefreshWeaponImage(Sprite sprite)
    {
        _weaponImage.sprite = sprite;
    }
}
