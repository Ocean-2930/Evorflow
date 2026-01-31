using Unity.VisualScripting;
using UnityEngine;

public class PartyToken : UnitToken, ICustomMouseInterface
{
    private GameObject benchTable;

    private void Awake()
    {
        ICustomMouseInterface k = this;
        k.OpenInterface(gameObject);
    }

    public void Initialize(GameObject obj, Unit unit)
    {
        benchTable = obj;
        SetUnit(unit);
    }

    public void OnLeftClick()
    {
        benchTable.GetComponent<BenchTable>().SwitchParty(unit);
    }
}
