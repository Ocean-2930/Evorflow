using UnityEngine;

public class ActivateButton : MonoBehaviour, ICustomMouseInterface
{
    [SerializeField] private GameObject[] gameObjects = new GameObject[0];
    private bool active = false;

    public void OnLeftClick()
    {
        active = !active;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(active);
        }
    }
}
