using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();

                if(instance == null)//파인드로 한번 찾았는데도 널이라면
                {
                    string name = typeof(T).ToString();
                    instance = new GameObject(name).AddComponent<T>();
                }
                
                DontDestroyOnLoad(instance.gameObject);
            }


            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }
}
