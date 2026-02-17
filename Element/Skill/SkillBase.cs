using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BattleEvent : UnityEvent<BattleFlow> { }

public abstract class SkillBase : ScriptableObject
{
    //return -1: empty, 0: unusable, 1: usable

    [SerializeField] private string _code;
    public string code { get { return _code; } }

    [SerializeField] private Sprite _icon;
    public Sprite icon { get { return _icon; } }

    [SerializeField] private string[] _tags;
    public string[] tags { get { return _tags; } }

    public bool isStatBoost { get { return _isStatBoost; } }
    [System.Serializable]
    private class BoostValues
    {
        public int MAXHP = 0;
        public int MAXCON = 0;
        public int STR = 0;
        public int INT = 0;
        public int AGI = 0;
        public int VIT = 0;

        public EnumIntArray<StatType> Values()
        {
            EnumIntArray<StatType> res = new EnumIntArray<StatType>();
            res[StatType.MAXHP] = MAXHP;
            res[StatType.MAXCON] = MAXCON;
            res[StatType.STR] = STR;
            res[StatType.INT] = INT;
            res[StatType.AGI] = AGI;
            res[StatType.VIT] = VIT;

            return new EnumIntArray<StatType>();
        }
    }
    [SerializeField] private BoostValues boostValues;
    [SerializeField] private bool _isStatBoost = false;


    public virtual EnumIntArray<StatType> StatBoost(EnumIntArray<StatType> statpipe)
    {
        return statpipe + boostValues.Values();
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
