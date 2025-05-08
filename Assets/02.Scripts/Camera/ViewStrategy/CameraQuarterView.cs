using UnityEngine;

public class CameraQuarterView : ICameraViewStrategy
{
    private readonly Vector3 _positionMargin = new Vector3(0f, 10f, -10f);
    
    private Transform _camera;
    private Transform _player;
    
    public CameraQuarterView(Transform camera, Transform player)
    {
        _camera = camera;
        _player = player;
    }

    public void Reset()
    {
        
    }

    public void View()
    {
        _camera.position = _player.position + _positionMargin;
        _camera.LookAt(_player);
    }
}
