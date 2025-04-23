using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
 
    private static System.Action<T> _onAwake;
 
    /// <summary>
    /// 생성 직후 이벤트 Handler 등록
    /// </summary>
    /// <param name="action"></param>
    public static void WhenInstantiated(System.Action<T> action)
    {
        if (Instance != null)
            action(Instance);
        else
            _onAwake += action;
    }
 
    protected virtual void Awake()
    {
        if (!enabled)
            return;
 
        if (Instance != null)
        {
            Debug.LogWarning($"Another instance of Singleton {typeof(T).Name} is being instantiated, destroying...", this);
            Destroy(gameObject);
            return;
        }
    
        Instance = (T)this;
 
        InternalAwake();
 
        _onAwake?.Invoke(Instance);
        _onAwake = null;
    }
 
    protected void OnEnable()
    {
        if (Instance != this)
            Awake();
    }
 
    protected virtual void InternalAwake() { }
}