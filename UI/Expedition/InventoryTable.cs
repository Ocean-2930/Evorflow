using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryTable : CustomMouseInterface
{
    public static InventoryTable inst
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            GameObject temp = GameObject.Find("InventoryTable");

            if (temp == null)
            {
                return null;
            }

            instance = temp.GetComponent<InventoryTable>();
            return instance;
        }
    }

    public static InventoryTable instance;

    [SerializeField] private GameObject iconHolder;
    [SerializeField] private GameObject itemIcon;

    private List<GameObject> inv = new List<GameObject>();
    private bool popup = false;

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

    public override void OnLeftClick()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (popup ? 720 : 280), 0);
        popup = !popup;
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
