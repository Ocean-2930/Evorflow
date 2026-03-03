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

    private int TURNCOUNTERMAX = 10000;

    public List<int> turnCounterFriendly;
    public List<int> turnCounterEnemy;

    private List<int> _turnCounterSpeed = new List<int> { 125, 156, 200, 250, 312, 400, 500, 624 };

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

    private int TurnCounterSpeed(int agi)
    {
        int index = agi - 1;
        if (index < 0) { index = 0; }

        while (_turnCounterSpeed.Count <= index)
        {
            int nextIndex = _turnCounterSpeed.Count;
            _turnCounterSpeed.Add(_turnCounterSpeed[nextIndex - 3] * 2);
        }

        return _turnCounterSpeed[index];
    }

    private UnitInst_Battle PopUnit()
    {
        UnitInst_Battle selectedUnit = null;
        bool selectedIsEnemy = false;
        int selectedIndex = -1;
        int minReadyCounter = int.MaxValue;

        for (int i = 0; i < unitFriendly.Count && i < turnCounterFriendly.Count; i++)
        {
            int c = turnCounterFriendly[i];
            if (c <= 0 && c < minReadyCounter)
            {
                minReadyCounter = c;
                selectedUnit = unitFriendly[i];
                selectedIsEnemy = false;
                selectedIndex = i;
            }
        }

        for (int i = 0; i < unitEnemy.Count && i < turnCounterEnemy.Count; i++)
        {
            int c = turnCounterEnemy[i];
            if (c <= 0 && c < minReadyCounter)
            {
                minReadyCounter = c;
                selectedUnit = unitEnemy[i];
                selectedIsEnemy = true;
                selectedIndex = i;
            }
        }

        if (selectedUnit == null)
        {
            return null;
        }

        if (selectedIsEnemy)
        {
            turnCounterEnemy[selectedIndex] += TURNCOUNTERMAX;
        }
        else
        {
            turnCounterFriendly[selectedIndex] += TURNCOUNTERMAX;
        }

        return selectedUnit;
    }

    private UnitInst_Battle NextTrun()
    {
        UnitInst_Battle selectedUnit = PopUnit();
        if (selectedUnit != null)
        {
            return selectedUnit;
        }

        List<int> friendlySpeed = new List<int>();
        List<int> enemySpeed = new List<int>();

        for (int i = 0; i < unitFriendly.Count && i < turnCounterFriendly.Count; i++)
        {
            friendlySpeed.Add(TurnCounterSpeed(unitFriendly[i].stat[StatType.AGI]));
        }

        for (int i = 0; i < unitEnemy.Count && i < turnCounterEnemy.Count; i++)
        {
            enemySpeed.Add(TurnCounterSpeed(unitEnemy[i].stat[StatType.AGI]));
        }

        int needTick = int.MaxValue;

        for (int i = 0; i < turnCounterFriendly.Count && i < friendlySpeed.Count; i++)
        {
            int c = turnCounterFriendly[i];
            if (c > 0)
            {
                int speed = friendlySpeed[i];
                int tick = c / speed;
                if (c % speed != 0)
                {
                    tick += 1;
                }

                if (tick < needTick)
                {
                    needTick = tick;
                }
            }
        }

        for (int i = 0; i < turnCounterEnemy.Count && i < enemySpeed.Count; i++)
        {
            int c = turnCounterEnemy[i];
            if (c > 0)
            {
                int speed = enemySpeed[i];
                int tick = c / speed;
                if (c % speed != 0)
                {
                    tick += 1;
                }

                if (tick < needTick)
                {
                    needTick = tick;
                }
            }
        }

        if (needTick == int.MaxValue)
        {
            needTick = 1;
        }

        for (int i = 0; i < turnCounterFriendly.Count && i < friendlySpeed.Count; i++)
        {
            turnCounterFriendly[i] -= needTick * friendlySpeed[i];
        }

        for (int i = 0; i < turnCounterEnemy.Count && i < enemySpeed.Count; i++)
        {
            turnCounterEnemy[i] -= needTick * enemySpeed[i];
        }

        return PopUnit();
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

        turnCounterFriendly = new List<int>();
        turnCounterEnemy = new List<int>();

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
