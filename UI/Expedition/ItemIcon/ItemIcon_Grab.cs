using UnityEngine;

public class ItemIcon_Grab : ItemIcon_Base
{
    public ItemInst bagInst;

    public void ItemReceived()
    {
        ExpeditonInven.inst.TakeItem(bagInst, itemInstance.amount);
        InventoryTable.inst.UpdateItems();
    }

    public override void OnDragRelease()
    {
        Destroy(gameObject);
    }
}
