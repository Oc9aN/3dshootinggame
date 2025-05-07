using System;
using UnityEngine;

public class PlayerRotate : PlayerComponent
{
    // 카메라 각도는 0도에서 시작한다. 밑에 변수는 그 값.
    private IRotationStrategy _rotationStrategy;
    
    private CrosshairRotationStrategy _crosshairRotationStrategy;
    private CursorRotationStrategy _cursorRotationStrategy;

    protected override void Awake()
    {
        base.Awake();
        _crosshairRotationStrategy = new CrosshairRotationStrategy();
        _cursorRotationStrategy =  new CursorRotationStrategy();

        _rotationStrategy = _crosshairRotationStrategy;
    }

    private void Start()
    {
        ViewManager.Instance.OnViewChanged += OnViewChanged;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.eulerAngles = new Vector3(0f, _rotationStrategy.RotationAngle(Player), 0f);
    }

    private void OnViewChanged(EViewType viewType)
    {
        if (viewType == EViewType.FirstPerson || viewType == EViewType.ThirdPerson)
        {
            _rotationStrategy = _crosshairRotationStrategy;
        }
        else
        {
            _rotationStrategy = _cursorRotationStrategy;
        }
    }
}