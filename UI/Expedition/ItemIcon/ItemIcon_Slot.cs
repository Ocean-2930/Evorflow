using UnityEngine;

public class UnitItemSlot : ItemIcon_Base
{
    private enum SlotType
    {
        Weapon = 0,
        Armor = 1,
        Supply1 = 2,
        Supply2 = 3
    }

    [SerializeField] private GameObject unitCard;
    [SerializeField] private SlotType type;

    public void RemoveItem()
    {
        if (itemInstance == null)
        {
            return;
        }

        Unit itemHolder = unitCard.GetComponent<UnitCard>().unit;
        ExpeditonInven.inst.AddItem(itemInstance);
        InventoryTable.inst.UpdateItems();
        if (type == SlotType.Weapon)
        {
            itemHolder.weapon = null;
        }
        else if (type == SlotType.Armor)
        {
            itemHolder.armor = null;
        }
        else if (type == SlotType.Supply1)
        {
            itemHolder.supply_1 = null;
        }
        else if (type == SlotType.Supply2)
        {
            itemHolder.supply_2 = null;
        }
        ResetSlot();
    }

    public void OnLeftClick()
    {
        RemoveItem();
    }

    public void OnDragRecieve(GameObject obj)
    {
        ItemIcon_Grab recieve = obj.GetComponent<ItemIcon_Grab>();

        if (recieve == null)
        {
            return;
        }

        ItemInst instance = recieve.itemInstance;
        UnitCard card  = unitCard.GetComponent<UnitCard>();
        if (type == SlotType.Weapon)
        {
            if (instance.item.type != ItemType.weapon)
            {
                return;
            }
            RemoveItem();            
            card.unit.weapon = instance;
        }
        else if (type == SlotType.Armor)
        {
            if (instance.item.type != ItemType.armor)
            {
                return;
            }
            RemoveItem();            
            card.unit.armor = instance;
        }
        else if(type == SlotType.Supply1)
        {
            if (instance.item.type != ItemType.supply)
            {
                return;
            }
            RemoveItem();
            card.unit.supply_1 = instance;
        }
        else if (type == SlotType.Supply2)
        {
            if (instance.item.type != ItemType.supply)
            {
                return;
            }
            RemoveItem();
            card.unit.supply_2 = instance;
        }
        SetItem(instance);
        recieve.ItemReceived();
    }
}
