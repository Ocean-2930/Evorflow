using UnityEngine;

[CreateAssetMenu(fileName = "Item_GSB", menuName = "Scriptable Objects/Item/Item_GSB")]
public class Item_GSB : Item
{
    [SerializeField] private EnumIntArray<StatType> boost = new EnumIntArray<StatType>();
}

