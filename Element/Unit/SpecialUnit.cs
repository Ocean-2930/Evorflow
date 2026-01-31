using UnityEngine;

[CreateAssetMenu(fileName = "SpecialUnit", menuName = "Scriptable Objects/Unit/SpecialUnit")]

public class SpecialUnit : UnitBase
{
    public Skill baseSkill = null;
    public Skill skill1 = null;
    public Skill skill2 = null;
    public Skill skill3 = null;
}
