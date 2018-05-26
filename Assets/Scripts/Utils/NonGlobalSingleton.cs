using UnityEngine;

/// <summary>
/// it acts as a singleton, but without "providing the global point of access" - which i don't want to
/// it only ensures that only a single instance is created, using the static variable for that
/// </summary>
public class NonGlobalSingleton<T>: MonoBehaviour where T : Component
{
    static T instance;

    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError($"more than one instance of {typeof(T)} was created!");
            Destroy(gameObject);
        }
    }
}