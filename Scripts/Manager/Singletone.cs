using UnityEngine;
using System.Collections.Generic;

public class MonoSingletone<T> : MonoBehaviour where T : MonoSingletone<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("Singletone" + typeof(T).ToString());
                    _instance = go.AddComponent<T>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

}

public class Singletone<T> where T : class
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = new type;
            }

            return _instance;
        }
    }
}