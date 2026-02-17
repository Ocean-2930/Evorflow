using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct DamageBox
{
    public int damage;
    public int heal;
    public int buff;
}

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public abstract class Skill : SkillBase
{
}
