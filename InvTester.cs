using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvTester : MonoBehaviour
{
    public string[] units;
    public string[] itemtype;
    public int[] itemamount;

    public void StartTest()
    {
        for (int i = 0; i < units.Length; i++)
        {
            ExpeditonInven.inst.units[i] = new UnitInst(UnitCloud.inst.specialUnit[units[i]]);
        }

        for (int i = 0; i < itemtype.Length; i++)
        {
            ItemData data = new ItemData();
            data.code = itemtype[i];
            data.amount = itemamount[i];

            ExpeditonInven.inst.AddItem(new ItemInst(data));
        }

        SceneLoader.inst.LoadScene(SceneName.Expedition);
    }
}
