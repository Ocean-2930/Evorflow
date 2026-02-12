using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UnitCard : MonoBehaviour, ICustomMouseInterface
{
    [SerializeField] protected GameObject cardIllust;
    [SerializeField] protected GameObject itemSlot;
    [SerializeField] protected GameObject cardName;
    [SerializeField] protected GameObject statPopup;

    public Unit unit;

    protected bool cardside = true;

    void Start()
    {
        cardside = true;
        itemSlot.SetActive(false);
    }

    public void SetUnit(Unit inunit)
    {
        unit = inunit;
        Image unitIllust = cardIllust.GetComponent<Image>();
        unitIllust.sprite = inunit.illust;
        unitIllust.SetNativeSize();
    }

    

    public void OnEnter()
    {
        GameObject obj = Instantiate(statPopup, Vector3.zero, Quaternion.identity);
        obj.GetComponent<StatusField>().UpdateStatus(unit);
        MousePopup.inst.ViewPopup(obj);
    }

    public void OnExit()
    {
        MousePopup.inst.CleanPopup();
    }

    public void CallUnitInfoScreen()
    {
        unit.CallUnitInfoScreen();
    }
}
