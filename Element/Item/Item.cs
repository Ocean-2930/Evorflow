using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;

#region enum-type
public enum ItemType
{
    None = 0,
    weapon = 1,
    armor = 2,
    supply = 3, 
    consume = 4,
    baseuse = 5
}

public enum StackType
{
    None = 0,
    durability = 1,
    stack = 2
}

public enum PartType
{
    food = 0,
    parts = 1,
    Mu7 = 2,
    Rho3 = 3,
    genebooster = 4,
    cells = 5,
    Quantum = 6,
    Warpmatter = 7,
    Ether = 8,
    Radiomass = 9,
    Tachyon = 10
}

public enum ItemTag
{
    Gun = 0
}
#endregion

#region Item
public abstract class Item : SkillBase
{
    public int code;
    public Sprite icon;

    public ItemType type;

    public StackType stackType = StackType.None;
    public int stackMAXCnt = 1;

    public EnumIntArray<PartType> parts = new EnumIntArray<PartType>();
    public virtual int value { get { return parts.Sum(); } }

    public List<ItemTag> tags;    

    public ItemInst GetInst()
    {
        ItemInst rinst = new ItemInst();
        rinst.item = this;
        rinst.amount = 1;
        return rinst;
    }

    public ItemInst GetInst(int amount)
    {
        ItemInst rinst = new ItemInst();
        rinst.item = this;
        rinst.amount = amount;
        return rinst;
    }
}

public class ItemInst
{
    public Item item;
    public int amount;

    public ItemInst()
    {

    }

    public ItemInst(ItemData data)
    {
        item = ItemCloud.inst.itemList[data.code];

        if (item.stackType == StackType.None)
        {
            amount = 1;
        }
        else
        {
            amount = data.amount;
        }
    }
}

public class ItemInstList : LinkedList<ItemInst>
{
    public List<ItemInst> asList
    {
        get
        {
            List<ItemInst> rlist = new List<ItemInst>();
            LinkedListNode<ItemInst> buff = First;
            while(buff != null)
            {
                rlist.Add(buff.Value);
                buff = buff.Next;
            }
            return rlist;
        }
    }

    public void Add(ItemInst getItem)
    {
        LinkedListNode<ItemInst> buff = First;
        while (buff != null)
        {
            if (buff.Value.item.code < getItem.item.code)
            {
                buff = buff.Next;
                continue;
            }

            if (getItem.item.code < buff.Value.item.code)
            {
                break;
            }

            if (getItem.item.stackType == StackType.None)
            {
                break;
            }

            if (getItem.item.stackType == StackType.durability)
            {
                if (buff.Value.amount <= getItem.amount)
                {
                    break;
                }
            }
            else if (getItem.item.stackType == StackType.stack)
            {
                buff.Value.amount += getItem.amount;

                if (buff.Value.item.stackMAXCnt < buff.Value.amount)
                {
                    getItem.amount = buff.Value.amount - buff.Value.item.stackMAXCnt;
                    buff.Value.amount = buff.Value.item.stackMAXCnt;
                }
                else
                {
                    return;
                }
            }

            buff = buff.Next;
        }

        if (buff == null)
        {
            AddLast(getItem);
        }
        else
        {
            AddBefore(buff, getItem);
        }
    }

    public void TakeItem(ItemInst from, int amount)
    {
        if (from.item.stackType != StackType.stack)
        {
            Remove(from);
            return;
        }

        LinkedListNode<ItemInst> buff = Find(from);
        from.amount -= amount;
        while (buff.Next != null)
        {
            if (buff.Value.item.code != buff.Next.Value.item.code)
            {
                break;
            }

            buff.Value.amount += buff.Next.Value.amount;
            buff.Next.Value.amount = 0;

            if(buff.Value.item.stackMAXCnt < buff.Value.amount)
            {
                buff.Next.Value.amount = buff.Value.amount - buff.Value.item.stackMAXCnt;
                buff.Value.amount = buff.Value.item.stackMAXCnt;
            }

            buff = buff.Next;
        }

        if (buff.Value.amount == 0)
        {
            Remove(buff);
        }
    }

    public static ItemInstList operator +(ItemInstList a, ItemInstList b)
    {
        LinkedListNode<ItemInst> buff = b.First;
        while(buff != null)
        {
            a.Add(buff.Value);
            buff = buff.Next;
        }
        return a;
    }
}
#endregion
