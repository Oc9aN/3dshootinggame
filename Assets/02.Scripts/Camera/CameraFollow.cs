using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private void Update()
    {
        // 보간 기법
        transform.position = _target.position;
    }
}
