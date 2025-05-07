using System;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    [SerializeField]
    private EParticleType _type;
    
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleSystemStopped()
    {
        Pool_Particle.Instance.ReturnPooledObject(_particleSystem, _type);
    }
}