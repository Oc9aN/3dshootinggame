using System;
using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private Light _light;

    private Weapon _weapon;
    
    private IEnumerator _lightCoroutine;

    private void Awake()
    {
        _light = GetComponentInChildren<Light>();
        _weapon = GetComponentInParent<Weapon>();

        _weapon.OnAttack += Flash;
    }

    private void Flash()
    {
        if (!ReferenceEquals(_lightCoroutine, null))
        {
            return;
        }
        _lightCoroutine = Light_Coroutine();
        StartCoroutine(_lightCoroutine);
    }

    private IEnumerator Light_Coroutine()
    {
        _light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _light.enabled = false;
        _lightCoroutine = null;
    }
}
