using System.Collections.Generic;
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

public enum BattleTeam
{
    friendly = 0,
    enemy = 1,
    neutral = 2
}

public class UnitInst
{
    private Unit_Scriptable _unitSource;
    public Unit_Scriptable unitSource { get { return _unitSource; } }

    private EnumIntArray<StatType> _baseStat = new EnumIntArray<StatType>();
    public EnumIntArray<StatType> baseStat { get { return _baseStat; } }

    public string code { get { return _unitSource.code; } }

    public Sprite illust { get { return _unitSource.illust; } }

    private Skill[] _skills = new Skill[4];
    protected Skill[] skills { get { return _skills; } }

    private ItemInst[] _items = new ItemInst[4];
    protected ItemInst[] items { get { return _items; } }

    public EnumIntArray<StatType> stat
    {
        get
        {
            EnumIntArray<StatType> rarr = new EnumIntArray<StatType>();

            for (int i = 0; i < _skills.Length; i++)
            {
                if (_skills[i] != null && _skills[i].isStatBoost)
                {
                    rarr = _skills[i].StatBoost(rarr);
                }
            }

            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].item.isStatBoost)
                {
                    rarr = _items[i].item.StatBoost(rarr);
                }
            }

            return rarr + _baseStat;
        }
    }

    public UnitInst()
    {

    }

    public UnitInst(SpecialUnit unit)
    {
        _unitSource = unit;

        _baseStat = unit.baseStat;
        baseSkill = unit.baseSkill;
        skill1 = unit.skill1;
        skill2 = unit.skill2;
        skill3 = unit.skill3;
    }

    public UnitInst(UnitInst_Battle inst)
    {
        CopyFrom(inst);
    }

    public UnitInst(UnitData data)
    {
        _unitSource = UnitCloud.inst.GetUnit(data.code);

        _baseStat[(int)StatType.MAXHP] = data.MAXHP;
        _baseStat[(int)StatType.HP] = data.HP;
        _baseStat[(int)StatType.MAXCON] = data.MAXCON;
        _baseStat[(int)StatType.CON] = data.CON;
        _baseStat[(int)StatType.STR] = data.STR;
        _baseStat[(int)StatType.INT] = data.INT;
        _baseStat[(int)StatType.AGI] = data.AGI;
        _baseStat[(int)StatType.VIT] = data.VIT;

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


    protected void CopyFrom(UnitInst inst)
    {
        _unitSource = inst.unitSource;

        _baseStat = new EnumIntArray<StatType>();
        for (int i = 0; i < _baseStat.length; i++)
        {
            _baseStat[i] = inst.baseStat[i];
        }

        for (int i = 0; i < _skills.Length; i++)
        {
            _skills[i] = inst.skills[i];
        }

        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = inst.items[i] == null ? null : new ItemInst(inst.items[i]);
        }
    }

    public Skill baseSkill { get { return _skills[0]; } set { _skills[0] = value; } }

    public Skill skill1 { get { return _skills[1]; } set { _skills[1] = value; } }

    public Skill skill2 { get { return _skills[2]; } set { _skills[2] = value; } }

    public Skill skill3 { get { return _skills[3]; } set { _skills[3] = value; } }

    public ItemInst weapon { get { return _items[0]; } set { _items[0] = value; } }

    public ItemInst armor { get { return _items[1]; } set { _items[1] = value; } }

    public ItemInst supply_1 { get { return _items[2]; } set { _items[2] = value; } }

    public ItemInst supply_2 { get { return _items[3]; } set { _items[3] = value; } }

    //buff

    public void CallUnitInfoScreen()
    {
        Debug.Log("info screen printed");
    }
}

public class UnitInst_Battle : UnitInst
{
    //Buff needed
    private BattleTeam _team;
    public BattleTeam team { get { return _team; } }

    public UnitInst_Battle(UnitInst inst)
    {
        CopyFrom(inst);
    }

    public SkillBase[] GetActives()
    {
        List<SkillBase> rlist = new List<SkillBase>();

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] != null && skills[i].hasActive)
            {
                rlist.Add(skills[i]);
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].item != null && items[i].item.hasActive)
            {
                rlist.Add(items[i].item);
            }
        }

        return rlist.ToArray();
    }

    public SkillBase[] GetPassive()
    {
        List<SkillBase> rlist = new List<SkillBase>();

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] != null && skills[i].hasPassive)
            {
                rlist.Add(skills[i]);
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].item != null && items[i].item.hasPassive)
            {
                rlist.Add(items[i].item);
            }
        }

        return rlist.ToArray();
    }
}
