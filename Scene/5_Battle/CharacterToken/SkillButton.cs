using UnityEngine;

public class SkillButton : MonoBehaviour, ICustomMouseInterface
{
    public SkillBase skillBase;

    public void OnLeftClick()
    {
        Debug.Log("Hello");
    }
}
