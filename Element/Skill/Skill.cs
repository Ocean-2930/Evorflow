using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public struct DamageBox
{
    public int damage;
    public int heal;
    public int buff;
}

public abstract class SkillBase : ScriptableObject
{
    //return -1: empty, 0: unusable, 1: usable

    public virtual bool isStatBoost { get { return false; } }
    public virtual EnumIntArray<StatType> StatBoost(Table table, UnitInst unit, ItemInst iteminst, EnumIntArray<StatType> statpipe)
    {
        return statpipe;
    }

    //public virtual int ScoutPassive()
    //public virtual int ScoutActive()

    //public virtual int SearchPassive()
    //public virtual int SearchActive()

    public virtual int isAttackPassive(UnitInst unit, ItemInst itemInst, DamageBox damagepipe)
    {
        return -1;
    }
    public virtual DamageBox VirtualAttackPassive(UnitInst unit, ItemInst itemInst, DamageBox damagepipe)
    {
        return new DamageBox();
    }
    public virtual DamageBox AttackPassive(UnitInst unit, ItemInst itemInst, DamageBox damagepipe)
    {
        return VirtualAttackPassive(unit, itemInst, damagepipe);
    }

    public virtual int isAttackActive(UnitInst unit, ItemInst itemInst, List<UnitInst> target)
    {
        return -1;
    }
    public virtual DamageBox VirtualAttackActive(UnitInst unit, ItemInst itemInst, List<UnitInst> target)
    {
        return new DamageBox();
    }
    public virtual DamageBox AttackActive(UnitInst unit, ItemInst itemInst, List<UnitInst> target)
    {
        return VirtualAttackActive(unit, itemInst, target);
    }
}

public abstract class Skill : SkillBase
{
    public string code;
    public Sprite icon;
}
