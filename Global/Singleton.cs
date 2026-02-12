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

public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
    private static T instance;
    private static bool blockInst = false;

    public static T inst
    {
        get
        {
            if (blockInst)
            {
                return null;
            }

            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    obj.name = instance.className;
                }
            }

            return instance;
        }
    }

    public abstract string className { get; }

    private void OnApplicationQuit()
    {
        blockInst = true;
    }

    protected virtual void OnDestroy()
    {
        blockInst = true;

        if (instance == this)
        {
            instance = null;
        }
    }
}