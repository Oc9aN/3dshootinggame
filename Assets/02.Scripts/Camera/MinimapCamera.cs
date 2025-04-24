using System;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private const float Y_OFFSET = 10f;

    [SerializeField]
    private Transform _target;

    [Header("Option")]
    [SerializeField]
    private float _minZoom;

    [SerializeField]
    private float _maxZoom;

    private float _zoom;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _zoom = (_minZoom + _maxZoom) / 2;
        _camera.orthographicSize = _zoom;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = _target.position;
        newPosition.y += Y_OFFSET;
        transform.position = newPosition;

        Vector3 newEulerAngles = _target.eulerAngles;
        newEulerAngles.x = 90f;
        newEulerAngles.z = 0;
        transform.eulerAngles = newEulerAngles;
    }

    public void ZoomIn()
    {
        _zoom -= 1;
        _zoom = Mathf.Max(_minZoom, _zoom);
        _camera.orthographicSize = _zoom;
    }

    public void ZoomOut()
    {
        _zoom += 1;
        _zoom = Mathf.Min(_maxZoom, _zoom);
        _camera.orthographicSize = _zoom;
    }
}