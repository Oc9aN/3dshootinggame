using TMPro;
using UnityEngine;

public class UI_PlayerAmmo : MonoBehaviour
{
    private TextMeshProUGUI _ammoText;

    private void Awake()
    {
        _ammoText = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(int maxBombCount)
    {
        _ammoText.text = $"남은 총알 {maxBombCount}/{maxBombCount}";
    }

    public void RefreshAmmoText(int bombCount, int maxBombCount)
    {
        _ammoText.text = $"남은 총알 {bombCount}/{maxBombCount}";
    }
}
