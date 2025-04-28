using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public event Action OnReadyEnd;
    
    [SerializeField]
    private CanvasGroup _readyGroup;

    [SerializeField]
    private TextMeshProUGUI _readyText;
    
    [SerializeField]
    private float _readyTextChangeTime = 0.5f;
    
    [SerializeField]
    private float _readyImageFadeTime = 2f;

    private IEnumerator _readyCoroutine;

    public void Ready()
    {
        _readyCoroutine = Ready_Coroutine();
        StartCoroutine(_readyCoroutine);
    }

    private IEnumerator Ready_Coroutine()
    {
        StringBuilder ready = new StringBuilder(_readyText.text);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(_readyTextChangeTime);
            ready.Append(".");
            _readyText.text = ready.ToString();
        }

        while (_readyGroup.alpha > 0)
        {
            _readyGroup.alpha -= Time.deltaTime / _readyImageFadeTime;
            yield return null;
        }
        OnReadyEnd?.Invoke();
    }

    public void Run()
    {
        StopCoroutine(_readyCoroutine);
    }

    public void Over()
    {
        
    }
}
