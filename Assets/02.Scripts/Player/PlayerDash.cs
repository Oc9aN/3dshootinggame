using System;
using UnityEngine;

public class PlayerDash : PlayerComponent
{
    [SerializeField]
    private float _dashForce = 10f;
    // 짧게 빠른 이동
    private void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
}
