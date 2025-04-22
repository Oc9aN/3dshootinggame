using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : PlayerComponent
{
    [SerializeField]
    private float _dashTime = 0.5f;

    [SerializeField]
    private float _dashStamina = 20f;
    
    private void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.TryUseStamina(_dashStamina))
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        Player.IsDash = true;

        yield return new WaitForSeconds(_dashTime);

        Player.IsDash = false;
        Player.IsRecoverStamina = true;
    }
}
