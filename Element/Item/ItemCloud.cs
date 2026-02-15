using System.Collections.Generic;
using UnityEngine;

public class ItemCloud : Singleton<ItemCloud>
{
    public override string className { get { return "ItemCloud"; } }

    public Dictionary<int, Item> itemList = new Dictionary<int, Item>();

    private void Awake()
    {
        Item[] loadedItems = Resources.LoadAll<Item>("Asset/Item");
        for (int i = 0; i < loadedItems.Length; i++)
        {
            itemList[loadedItems[i].code] = loadedItems[i];
        }
    }
}
