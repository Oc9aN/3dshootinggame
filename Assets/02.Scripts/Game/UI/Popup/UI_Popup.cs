using System;
using DG.Tweening;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    private event Action _closeCallback;
    [Header("UI Options")]
    [SerializeField]
    private float _duration;
    
    private RectTransform _rectTransform;

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        PopupManager.Instance.AddPopup(this);
        gameObject.SetActive(false);
    }

    public bool Open(Action closeCallback)
    {
        if (gameObject.activeSelf)
        {
            return false;
        }

        _closeCallback = closeCallback;
        
        _rectTransform.DOKill();
        _rectTransform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        _rectTransform.DOScale(new Vector3(1f, 1f, 1f), _duration).SetUpdate(true).SetEase(Ease.Unset);
        
        return true;
    }
    
    public void Close()
    {
        _rectTransform.DOKill();
        _rectTransform.DOScale(new Vector3(0f, 0f, 1f), _duration).SetUpdate(true).SetEase(Ease.Unset)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                _closeCallback?.Invoke();
            });
        
    }
}