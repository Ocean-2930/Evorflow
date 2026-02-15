using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_000", menuName = "Scriptable Objects/Skill/GenralSkill/Skill_000")]
public class Skill_000 : Skill
{
    public override int isAttackActive(UnitInst unit, ItemInst itemInst, List<UnitInst> target)
    {
        return 1;
    }
    public override DamageBox AttackActive(UnitInst unit, ItemInst itemInst, List<UnitInst> target)
    {
        DamageBox rbox = new DamageBox();
        rbox.damage = unit.stat[StatType.STR] * 2;
        return rbox;
    }
}
