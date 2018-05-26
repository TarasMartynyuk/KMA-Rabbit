using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    var obj = new GameObject {name = typeof(T).Name};
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    static T instance;


    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
}