using UnityEngine;

public interface ICameraViewStrategy
{
    public void View(Transform camera, Transform target);
}
