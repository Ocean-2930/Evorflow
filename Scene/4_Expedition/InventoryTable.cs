using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryTable : SceneSingleton<InventoryTable>
{
    public override string className => "InventoryTable";

    [SerializeField] private GameObject iconHolder;
    [SerializeField] private GameObject itemIcon;

    private List<GameObject> inv = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < ExpeditonInven.inst.bagSize; i++)
        {
            GameObject buff = Instantiate(itemIcon);
            buff.transform.SetParent(iconHolder.transform);

            Vector3 pos = Vector3.zero;
            pos.x = i % 5 * 120 - 240;
            pos.y = - i / 5 * 120 + 120;
            buff.GetComponent<RectTransform>().anchoredPosition = pos;

            inv.Add(buff);
        }

        UpdateItems();
    }

    public void UpdateItems()
    {
        LinkedListNode<ItemInst> buff = ExpeditonInven.inst.items.First;
        for (int i = 0; i < inv.Count; i++)
        {
            if (buff == null)
            {
                inv[i].GetComponent<ItemIcon_Bag>().ResetSlot();
            }
            else
            {
                inv[i].GetComponent<ItemIcon_Bag>().SetItem(buff.Value);
                buff = buff.Next;
            }
        }
    }
}
