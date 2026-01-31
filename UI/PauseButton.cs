using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void ExitGame()
    {
        SceneLoader.inst.ExitGame();
    }
}
