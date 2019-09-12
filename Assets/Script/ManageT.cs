using System.Threading;
using System.Collections;
using System;
using UnityEngine;

public class Singleton<T> where T : new()
{
    private static T s_singleton = default(T);
    private static object s_objectLock = new object();
    public static T singleton
    {
        get
        {
            if (Singleton<T>.s_singleton == null)
            {
                object obj;
                Monitor.Enter(obj = Singleton<T>.s_objectLock);//加锁防止多线程创建单例
                try
                {
                    if (Singleton<T>.s_singleton == null)
                    {
                        Singleton<T>.s_singleton = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));//创建单例的实例
                    }
                }
                finally
                {
                    Monitor.Exit(obj);
                }
            }
            return Singleton<T>.s_singleton;
        }
    }
    protected Singleton()
    {
    }

}
public class MonoSingletonX<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
 
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool applicationIsQuitting = false;
    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
 
                    }
                }
                return _instance;
            }
        }
    }

    ///////////////////////////////////////////

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Tick();
    }
    private void OnDestroy()
    {
        applicationIsQuitting = true;
        UnInit();
    }

    ///////////////////////////////////////////

    protected virtual void Init()
    {


    }

    protected virtual void UnInit()
    {


    }

    protected virtual void Tick()
    {


    }

}
