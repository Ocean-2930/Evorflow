using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitToken : MonoBehaviour
{
    [SerializeField] private GameObject illustField;

    protected UnitInst unit;

    public void SetUnit(UnitInst unit)
    {
        this.unit = unit;
        illustField.GetComponent<RectTransform>().sizeDelta = new Vector3(unit.illust.rect.width, unit.illust.rect.height, 0);
        illustField.GetComponent<Image>().sprite = unit.illust;
    }

    public bool TokenOf(UnitInst unit)
    {
        return this.unit == unit;
    }
}
