#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Opening = 0,
    Main = 1,
    Base = 2,
    Expedition = 3,
    Battle = 4
}

public class SceneLoader : Singleton<SceneLoader>
{
    public override string className { get { return "SceneLoader"; } }

    private AsyncOperation loader;
    private string[] sceneName = new string[]
    {
        "1_Opening",
        "2_Main",
        "3_Base",
        "4_Expedition",
        "5_Battle"
    };

    public void LoadScene(SceneName ind)
    {
        loader = SceneManager.LoadSceneAsync(sceneName[(int)ind]);
        loader.allowSceneActivation = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;  // 에디터에서 플레이 중단
#else
        Application.Quit(); // 빌드된 게임 종료
#endif
    }
}

/*
    private AsyncOperation loader;

    private string target_scene;

    void Start()
    {
        GameObject nc = GameObject.Find("Name_Carrier(Clone)");

        target_scene = nc.GetComponent<NameCarrier>().target_scene;

        Destroy(nc);

        StartCoroutine("Scene_Load");
    }

    IEnumerator Scene_Load()
    {
        loader = SceneManager.LoadSceneAsync(target_scene);

        loader.allowSceneActivation = false;

        while (loader.progress < 0.9f)
        {
            //progress bar

            yield return null;
        }

        loader.allowSceneActivation = true;

        yield return null;
    }
*/
