using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ExpeditonInven : Singleton<ExpeditonInven>
{
    public override string className { get { return "ExpeditionInven"; } }
    
    public UnitInst[] units = new UnitInst[5];
    public int bagSize = 15;
    public ItemInstList items = new ItemInstList();

    public void AddItem(ItemInst getItem)
    {
        items.Add(getItem);
    }

    public void TakeItem(ItemInst from, int amount)
    {
        items.TakeItem(from, amount);
    }

    public void RemoveItem(ItemInst removeItem)
    {
        items.Remove(removeItem);
    }

    public void ClearAll()
    {
        units = new UnitInst[5];
        items = new ItemInstList();
    }
}

