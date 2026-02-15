using Unity.VisualScripting;
using UnityEngine;

public class PartyToken : UnitToken, ICustomMouseInterface
{
    private GameObject benchTable;

    public void Initialize(GameObject obj, UnitInst unit)
    {
        benchTable = obj;
        SetUnit(unit);
    }

    public void OnLeftClick()
    {
        benchTable.GetComponent<BenchTable>().SwitchParty(unit);
    }
}
