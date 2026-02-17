using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private GameObject textField;
    [SerializeField] private float spacing;

    public void UpdateText(string text)
    {
        TextMeshProUGUI tmp = textField.GetComponent<TextMeshProUGUI>();
        tmp.text = text;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tmp.rectTransform);

        Vector3 size = gameObject.GetComponent<RectTransform>().sizeDelta;
        size.y = tmp.preferredHeight + spacing;
        gameObject.GetComponent<RectTransform>().sizeDelta = size;
    }
}
