using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : SceneSingleton<BattleControl>
{
    public override string className => "BattleControl";

    private IEnumerator _nextCoroutine;
    public bool endBattle = false;

    void Start()
    {
        _nextCoroutine = null;
        endBattle = false;
        StartCoroutine(MainLoop(new BattleFlow()));
    }

    public void NextCoroutine(IEnumerator co)
    {
        if (_nextCoroutine == null)
        {
            _nextCoroutine = co;
        }
    }

    private IEnumerator MainLoop(BattleFlow bflow)
    {
        while (true)
        {
            _nextCoroutine = null;
            yield return new WaitUntil(() => (_nextCoroutine != null || endBattle));

            if (_nextCoroutine != null) { yield return StartCoroutine(_nextCoroutine); }

            if (endBattle) { break; }
        }

        yield return null;
    }
}
