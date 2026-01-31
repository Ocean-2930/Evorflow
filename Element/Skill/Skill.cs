using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class SkillCloud : Singleton<SkillCloud>
{
    public override string className { get { return "SkillCloud"; } }

    public Dictionary<string, Skill> generalSkillList = new Dictionary<string, Skill>();
    public Dictionary<string, Skill> specialSkillList = new Dictionary<string, Skill>();

    private void Awake()
    {
        Skill[] loadedItems = Resources.LoadAll<Skill>("Asset/Skill/GeneralSkill");
        for (int i = 0; i < loadedItems.Length; i++)
        {
            generalSkillList[loadedItems[i].code] = loadedItems[i];
        }

        loadedItems = Resources.LoadAll<Skill>("Asset/Skill/SpecialSkill");
        for (int i = 0; i < loadedItems.Length; i++)
        {
            specialSkillList[loadedItems[i].code] = loadedItems[i];
        }
    }

    public Skill GetSkill(string code)
    {
        if (code[0] == 'S')
        {
            return specialSkillList[code];
        }
        else
        {
            return generalSkillList[code];
        }
    }
}

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
    public virtual EnumIntArray<StatType> StatBoost(Table table, Unit unit, ItemInst iteminst, EnumIntArray<StatType> statpipe)
    {
        return statpipe;
    }

    //public virtual int ScoutPassive()
    //public virtual int ScoutActive()

    //public virtual int SearchPassive()
    //public virtual int SearchActive()

    public virtual int isAttackPassive(Unit unit, ItemInst itemInst, DamageBox damagepipe)
    {
        return -1;
    }
    public virtual DamageBox VirtualAttackPassive(Unit unit, ItemInst itemInst, DamageBox damagepipe)
    {
        return new DamageBox();
    }
    public virtual DamageBox AttackPassive(Unit unit, ItemInst itemInst, DamageBox damagepipe)
    {
        return VirtualAttackPassive(unit, itemInst, damagepipe);
    }

    public virtual int isAttackActive(Unit unit, ItemInst itemInst, List<Unit> target)
    {
        return -1;
    }
    public virtual DamageBox VirtualAttackActive(Unit unit, ItemInst itemInst, List<Unit> target)
    {
        return new DamageBox();
    }
    public virtual DamageBox AttackActive(Unit unit, ItemInst itemInst, List<Unit> target)
    {
        return VirtualAttackActive(unit, itemInst, target);
    }
}

public abstract class Skill : SkillBase
{
    public string code;
    public Sprite icon;
}
