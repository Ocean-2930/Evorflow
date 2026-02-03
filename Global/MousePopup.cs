using UnityEngine;
using UnityEngine.InputSystem;

public class MousePopup : SceneSingleton<MousePopup>
{
    public override string className => "MousePopup";

    public static MousePopup instance;

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gameObject.transform.parent as RectTransform,
            mousePos,
            null,
            out localPos
        );
        gameObject.GetComponent<RectTransform>().localPosition = localPos;
    }

    public void ViewPopup(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void CleanPopup()
    {
        int i = transform.childCount - 1;
        while (0 <= i)
        {
            Destroy(transform.GetChild(i).gameObject);
            i--;
        }
    }
}
