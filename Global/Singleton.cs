using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T inst
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            GameObject obj = new GameObject();
            instance = obj.AddComponent<T>();
            obj.name = instance.className;
            DontDestroyOnLoad(obj);
            return instance;
        }
    }

    private static T instance;

    public abstract string className { get; }
}
