using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFlow
{
    #region Active Check
    public bool activeUseable;
    public bool ActiveCkeck(SkillBase sb)
    {
        activeUseable = true;
        sb.ExecuteActiveCheck(this);
        return activeUseable;
    }
    #endregion

    #region Active Selector
    public IEnumerator activeSelector;
    public IEnumerator ActiveSelect(SkillBase sb)
    {
        activeSelector = null;
        sb.ExecuteActiveSelector(this);
        return activeSelector;
    }
    #endregion

    #region Active Skill
    // sb.ExecuteActiveSkill();
    #endregion
}
