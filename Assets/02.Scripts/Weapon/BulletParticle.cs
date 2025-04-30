using System;
using System.Collections;
using UnityEngine;

public class BulletParticle : MonoBehaviour, IPoolObject
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void Play(Vector3 position, Vector3 normal)
    {
        transform.position = position;
        transform.forward = normal;
        gameObject.SetActive(true);
        _particle.Play();
    }

    public void Initialize()
    {
        _particle.Stop();
    }

    private void OnParticleSystemStopped()
    {
        Pool_BulletEffect.Instance.ReturnPooledObject(this);
    }
}