using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionBox : MonoBehaviour, ICustomMouseInterface
{
    [SerializeField] private GameObject maskField;
    [SerializeField] private GameObject textField;

    private System.Action<int> reportTo;
    private int optionIndex = -1;

    public void Initialize(System.Action<int> from, int opindex, string text)
    {
        reportTo = from;
        optionIndex = opindex;
        UpdateText(text);
    }

    public void UpdateText(string text)
    {
        TextMeshProUGUI tmp = textField.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
    }

    public void OnHover()
    {
        maskField.GetComponent<Image>().color = 0x3C3C3C.FromHex();
    }

    public void OnExit()
    {
        maskField.GetComponent<Image>().color = 0x000000.FromHex();
    }

    public void OnLeftClick()
    {
        reportTo?.Invoke(optionIndex);
    }
}

public static class ColorExtend
{
    public static Color FromHex(this int hex)
    {
        return new Color(
            ((hex >> 16) & 0xFF) / 255f,
            ((hex >> 8) & 0xFF) / 255f,
            (hex & 0xFF) / 255f,
            1.0f
        );
    }
}