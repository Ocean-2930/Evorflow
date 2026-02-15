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
