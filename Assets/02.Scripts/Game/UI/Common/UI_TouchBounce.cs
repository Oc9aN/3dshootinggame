using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TouchBounce : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // 마우스 터치 시 크기 변경
    public float StartScale = 1f;
    public float EndScale = 1.2f;
    public float Duration = 0.5f;

    private RectTransform _rectTransform;
    private Vector2 _originPivot;
    private Vector2 _originAnchoredPosition;

    private bool _isLayout;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originPivot = _rectTransform.pivot;
        _originAnchoredPosition = _rectTransform.anchoredPosition;

        _isLayout = transform.parent.TryGetComponent(out LayoutGroup layoutGroup);
    }

    private void OnDisable()
    {
        _rectTransform.localScale = new Vector3(StartScale, StartScale, 1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isLayout)
        {
            _originAnchoredPosition = _rectTransform.anchoredPosition;
        }
        _rectTransform.DOKill();
        PivotChange();
        _rectTransform.DOScale(new Vector3(EndScale, EndScale, 1f), Duration).SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.DOKill();
        PivotChange();
        _rectTransform.DOScale(new Vector3(StartScale, StartScale, 1f), Duration).SetUpdate(true)
            .OnComplete(RetuningChange);
    }

    private void PivotChange()
    {
        _rectTransform.pivot = Vector2.one / 2f;
        var xPivotMargin = _rectTransform.pivot.x - _originPivot.x;
        var yPivotMargin = _rectTransform.pivot.y - _originPivot.y;
        var newPosition = _originAnchoredPosition;
        newPosition.x += _rectTransform.sizeDelta.x * xPivotMargin;
        newPosition.y += _rectTransform.sizeDelta.y * yPivotMargin;
        _rectTransform.anchoredPosition = newPosition;
    }

    private void RetuningChange()
    {
        _rectTransform.pivot = _originPivot;
        _rectTransform.anchoredPosition = _originAnchoredPosition;
    }
}