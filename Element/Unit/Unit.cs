using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

public enum StatType
{
    MAXHP = 0,
    HP = 1,
    MAXCON = 2,
    CON = 3,
    STR = 4,
    INT = 5,
    AGI = 6,
    VIT = 7
}

public class Unit
{
    public Table table;
        
    public EnumIntArray<StatType> stat
    {
        get
        {
            EnumIntArray<StatType> rarr = new EnumIntArray<StatType>();

            if (baseSkill != null && baseSkill.isStatBoost)
            {
                rarr = baseSkill.StatBoost(table, this, null, rarr);
            }
            if (skill1 != null && skill1.isStatBoost)
            {
                rarr = skill1.StatBoost(table, this, null, rarr);
            }
            if (skill2 != null && skill2.isStatBoost)
            {
                rarr = skill2.StatBoost(table, this, null, rarr);
            }
            if (skill3 != null && skill3.isStatBoost)
            {
                rarr = skill3.StatBoost(table, this, null, rarr);
            }

            if (weapon != null && weapon.item.isStatBoost)
            {
                rarr = weapon.item.StatBoost(table, this, weapon, rarr);
            }
            if (armor != null && armor.item.isStatBoost)
            {
                rarr = armor.item.StatBoost(table, this, armor, rarr);
            }
            if (supply_1 != null && supply_1.item.isStatBoost)
            {
                rarr = supply_1.item.StatBoost(table, this, supply_1, rarr);
            }
            if (supply_2 != null && supply_2.item.isStatBoost)
            {
                rarr = supply_2.item.StatBoost(table, this, supply_2, rarr);
            }

            return rarr + baseStat;
        }
    }

    private EnumIntArray<StatType> baseStat = new EnumIntArray<StatType>();

    public string code;
    public Sprite illust;

    public Unit()
    {

    }

    public Unit(SpecialUnit unit)
    {
        code = unit.code;
        illust = unit.illust;

        baseStat = unit.baseStat;
        baseSkill = unit.baseSkill;
        skill1 = unit.skill1;
        skill2 = unit.skill2;
        skill3 = unit.skill3;
    }

    public Unit(UnitData data)
    {
        code = data.code;
        illust = UnitCloud.inst.GetUnit(code).illust;

        baseStat[(int)StatType.MAXHP] = data.MAXHP;
        baseStat[(int)StatType.HP] = data.HP;
        baseStat[(int)StatType.MAXCON] = data.MAXCON;
        baseStat[(int)StatType.CON] = data.CON;
        baseStat[(int)StatType.STR] = data.STR;
        baseStat[(int)StatType.INT] = data.INT;
        baseStat[(int)StatType.AGI] = data.AGI;
        baseStat[(int)StatType.VIT] = data.VIT;

        if (data.baseSkill == "")
        {
            baseSkill = SkillCloud.inst.GetSkill("000");
        }
        else
        {
            baseSkill = SkillCloud.inst.GetSkill(data.baseSkill);
        }

        if (data.skill1 != "")
        {
            skill1 = SkillCloud.inst.GetSkill(data.skill1);
        }
        if (data.skill2 != "")
        {
            skill2 = SkillCloud.inst.GetSkill(data.skill2);
        }
        if (data.skill3 != "")
        {
            skill3 = SkillCloud.inst.GetSkill(data.skill3);
        }
    }

    public Skill baseSkill = null;
    public Skill skill1 = null;
    public Skill skill2 = null;
    public Skill skill3 = null;

    public ItemInst weapon = null;
    public ItemInst armor = null;
    public ItemInst supply_1 = null;
    public ItemInst supply_2 = null;

    //buff

    public void CallUnitInfoScreen()
    {
        Debug.Log("info screen printed");
    }
}
