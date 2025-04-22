using TMPro;
using UnityEngine;

public class UI_PlayerBomb : MonoBehaviour
{
    private TextMeshProUGUI _bombText;

    private void Awake()
    {
        _bombText = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(int maxBombCount)
    {
        _bombText.text = $"남은 폭탄 {maxBombCount}/{maxBombCount}";
    }

    public void RefreshBombText(int bombCount, int maxBombCount)
    {
        _bombText.text = $"남은 폭탄 {bombCount}/{maxBombCount}";
    }
}