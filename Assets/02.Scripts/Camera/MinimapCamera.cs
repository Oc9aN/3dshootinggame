using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _yOffset;

    private void LateUpdate()
    {
        Vector3 newPosition = _target.position;
        newPosition.y += _yOffset;
        transform.position = newPosition;
        
        Vector3 newEulerAngles = _target.eulerAngles;
        newEulerAngles.x = 90f;
        newEulerAngles.z = 0;
        transform.eulerAngles = newEulerAngles;
    }
}
