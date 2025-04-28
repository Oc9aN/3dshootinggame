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
    private TextMeshProUGUI _fadeText;
    
    [SerializeField]
    private float _readyTextChangeTime = 0.5f;
    
    [SerializeField]
    private float _readyImageFadeTime = 2f;

    private IEnumerator _readyCoroutine;
    private IEnumerator _overCoroutine;

    public void Ready()
    {
        _readyCoroutine = Ready_Coroutine();
        StartCoroutine(_readyCoroutine);
    }

    private IEnumerator Ready_Coroutine()
    {
        StringBuilder ready = new StringBuilder("Ready");
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(_readyTextChangeTime);
            ready.Append(".");
            _fadeText.text = ready.ToString();
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
        _fadeText.text = "Over";
        
        _overCoroutine = Over_Coroutine();
        StartCoroutine(_overCoroutine);
    }
    
    private IEnumerator Over_Coroutine()
    {
        while (_readyGroup.alpha < 1)
        {
            _readyGroup.alpha += Time.deltaTime / _readyImageFadeTime;
            yield return null;
        }
    }
}
