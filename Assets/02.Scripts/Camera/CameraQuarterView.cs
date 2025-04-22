using UnityEngine;

public class CameraQuarterView : ICameraViewStrategy
{
    private readonly Vector3 _positionMargin = new Vector3(0f, 10f, -10f);
    public void View(Transform camera, Transform target)
    {
        camera.position = target.position + _positionMargin;
        camera.LookAt(target);
    }
}
