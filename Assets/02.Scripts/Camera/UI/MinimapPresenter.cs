using System;
using UnityEngine;

public class MinimapPresenter : MonoBehaviour
{
    [SerializeField]
    private MinimapCamera _minimapCameraModel;
    
    private MinimapView _minimapView;

    private void Awake()
    {
        _minimapView = GetComponent<MinimapView>();
    }

    private void Start()
    {
        _minimapView.MinimapZoomInButton.onClick.AddListener(_minimapCameraModel.ZoomIn);
        _minimapView.MinimapZoomOutButton.onClick.AddListener(_minimapCameraModel.ZoomOut);
    }
}
