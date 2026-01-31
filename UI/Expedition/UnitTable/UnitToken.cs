using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitToken : MonoBehaviour
{
    [SerializeField] private GameObject illustField;

    protected Unit unit;

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
        illustField.GetComponent<RectTransform>().sizeDelta = new Vector3(unit.illust.rect.width, unit.illust.rect.height, 0);
        illustField.GetComponent<Image>().sprite = unit.illust;
    }

    public bool TokenOf(Unit unit)
    {
        return this.unit == unit;
    }
}
