using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : SceneSingleton<BattleControl>
{
    public override string className => "BattleControl";

    private List<UnitInst_Battle> backupFriendly;
    private List<UnitInst_Battle> backupEnemy;
    private List<UnitInst_Battle> unitFriendly;
    private List<UnitInst_Battle> unitEnemy;

    private float TURNCOUNTERMAX = 100f;

    public List<float> turnCounterFriendly;
    public List<float> turnCounterEnemy;

    private List<float> _turnCounterSpeed;

    private IEnumerator _nextCoroutine;
    public IEnumerator nextCoroutine
    {
        get
        {
            return _nextCoroutine;
        }
        set
        {
            if (value == null || _nextCoroutine == null)
            {
                _nextCoroutine = value;
            }
        }
    }
    public bool endBattle = false;

    void Start()
    {
        BattleInven inven = FindObjectOfType<BattleInven>();

        backupFriendly = new List<UnitInst_Battle>();
        backupEnemy = new List<UnitInst_Battle>();

        _turnCounterSpeed = new List<float>();
        float turnCounterSpeedValue = 1f;
        for (int i = 0; i < 8; i++)
        {
            _turnCounterSpeed.Add(turnCounterSpeedValue);
            turnCounterSpeedValue *= 1.25f;
        }

        if (inven != null)
        {
            if (inven.unitFriendly != null)
            {
                for (int i = 0; i < inven.unitFriendly.Count; i++)
                {
                    backupFriendly.Add(new UnitInst_Battle(inven.unitFriendly[i], BattleTeam.friendly));
                }
            }

            if (inven.unitEnemy != null)
            {
                for (int i = 0; i < inven.unitEnemy.Count; i++)
                {
                    backupEnemy.Add(new UnitInst_Battle(inven.unitEnemy[i], BattleTeam.enemy));
                }
            }
        }

        LoadBackup();

        nextCoroutine = null;
        endBattle = false;
        StartCoroutine(MainLoop(new BattleFlow()));
    }

    private float TurnCounterSpeed(int index)
    {
        while (_turnCounterSpeed.Count <= index)
        {
            _turnCounterSpeed.Add(_turnCounterSpeed[_turnCounterSpeed.Count - 1] * 1.25f);
        }

        return _turnCounterSpeed[index];
    }

    private UnitInst_Battle NextTrun()
    {
        UnitInst_Battle nextUnit = null;
        bool isEnemy = false;
        int nextIndex = -1;
        float minNeedCounter = float.MaxValue;

        for (int i = 0; i < unitFriendly.Count && i < turnCounterFriendly.Count; i++)
        {
            int speedIndex = unitFriendly[i].stat[StatType.AGI] - 1;
            if (speedIndex < 0) { speedIndex = 0; }

            float speed = TurnCounterSpeed(speedIndex);
            float needCounter = turnCounterFriendly[i] / speed;

            if (needCounter < minNeedCounter)
            {
                minNeedCounter = needCounter;
                nextUnit = unitFriendly[i];
                isEnemy = false;
                nextIndex = i;
            }
        }

        for (int i = 0; i < unitEnemy.Count && i < turnCounterEnemy.Count; i++)
        {
            int speedIndex = unitEnemy[i].stat[StatType.AGI] - 1;
            if (speedIndex < 0) { speedIndex = 0; }

            float speed = TurnCounterSpeed(speedIndex);
            float needCounter = turnCounterEnemy[i] / speed;

            if (needCounter < minNeedCounter)
            {
                minNeedCounter = needCounter;
                nextUnit = unitEnemy[i];
                isEnemy = true;
                nextIndex = i;
            }
        }

        if (nextUnit == null || nextIndex < 0)
        {
            return null;
        }

        int nextSpeedIndex = nextUnit.stat[StatType.AGI] - 1;
        if (nextSpeedIndex < 0) { nextSpeedIndex = 0; }

        float nextSpeed = TurnCounterSpeed(nextSpeedIndex);
        float nextCounter = isEnemy ? turnCounterEnemy[nextIndex] : turnCounterFriendly[nextIndex];
        int speedMultiply = Mathf.CeilToInt(nextCounter / nextSpeed);
        if (speedMultiply < 1)
        {
            speedMultiply = 1;
        }

        for (int i = 0; i < unitFriendly.Count && i < turnCounterFriendly.Count; i++)
        {
            int speedIndex = unitFriendly[i].stat[StatType.AGI] - 1;
            if (speedIndex < 0) { speedIndex = 0; }

            turnCounterFriendly[i] -= TurnCounterSpeed(speedIndex) * speedMultiply;
        }

        for (int i = 0; i < unitEnemy.Count && i < turnCounterEnemy.Count; i++)
        {
            int speedIndex = unitEnemy[i].stat[StatType.AGI] - 1;
            if (speedIndex < 0) { speedIndex = 0; }

            turnCounterEnemy[i] -= TurnCounterSpeed(speedIndex) * speedMultiply;
        }

        if (isEnemy)
        {
            turnCounterEnemy[nextIndex] += TURNCOUNTERMAX;
        }
        else
        {
            turnCounterFriendly[nextIndex] += TURNCOUNTERMAX;
        }

        return nextUnit;
    }

    public void LoadBackup()
    {
        if (backupFriendly == null || backupEnemy == null)
        {
            return;
        }

        if (unitFriendly == null)
        {
            unitFriendly = new List<UnitInst_Battle>();
        }
        if (unitEnemy == null)
        {
            unitEnemy = new List<UnitInst_Battle>();
        }

        unitFriendly.Clear();
        unitEnemy.Clear();

        for (int i = 0; i < backupFriendly.Count; i++)
        {
            unitFriendly.Add(new UnitInst_Battle(backupFriendly[i], BattleTeam.friendly));
        }

        for (int i = 0; i < backupEnemy.Count; i++)
        {
            unitEnemy.Add(new UnitInst_Battle(backupEnemy[i], BattleTeam.enemy));
        }

        turnCounterFriendly = new List<float>();
        turnCounterEnemy = new List<float>();

        for (int i = 0; i < unitFriendly.Count; i++)
        {
            turnCounterFriendly.Add(TURNCOUNTERMAX);
        }

        for (int i = 0; i < unitEnemy.Count; i++)
        {
            turnCounterEnemy.Add(TURNCOUNTERMAX);
        }
    }

    private IEnumerator MainLoop(BattleFlow bflow)
    {
        while (true)
        {
            nextCoroutine = null;
            yield return new WaitUntil(() => (nextCoroutine != null || endBattle));

            if (nextCoroutine != null) { yield return StartCoroutine(nextCoroutine); }

            if (endBattle) { break; }
        }

        yield return null;
    }
}
