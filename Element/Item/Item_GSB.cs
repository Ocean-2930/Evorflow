using UnityEngine;

[CreateAssetMenu(fileName = "Item_GSB", menuName = "Scriptable Objects/Item/Item_GSB")]
public class Item_GSB : Item
{
    [SerializeField] private EnumIntArray<StatType> boost = new EnumIntArray<StatType>();

    public override bool isStatBoost { get { return true; } }

    public override EnumIntArray<StatType> StatBoost(Table table, UnitInst unit, ItemInst itemInst, EnumIntArray<StatType> statpipe)
    {
        for (int i = 0; i < boost.length; i++)
        {
            statpipe[i] += boost[i];
        }
        return statpipe;
    }
}

