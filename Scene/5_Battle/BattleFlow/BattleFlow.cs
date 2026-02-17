using UnityEngine;

public class BattleFlow
{
    public bool activeUseable;

    public bool ActiveCkeck(SkillBase sb)
    {
        activeUseable = true;
        sb.ExecuteActiveCheck(this);
        return activeUseable;
    }
}
