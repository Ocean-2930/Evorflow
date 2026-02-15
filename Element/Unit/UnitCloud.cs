using System.Collections.Generic;
using UnityEngine;

public class UnitCloud : Singleton<UnitCloud>
{
    public override string className { get { return "UnitCloud"; } }

    public Dictionary<string, Unit_Scriptable> randomUnit = new Dictionary<string, Unit_Scriptable>();
    public Dictionary<string, SpecialUnit> specialUnit = new Dictionary<string, SpecialUnit>();

    private void Awake()
    {
        Unit_Scriptable[] loadedItems = Resources.LoadAll<Unit_Scriptable>("Asset/Unit/RandomUnit");
        for (int i = 0; i < loadedItems.Length; i++)
        {
            randomUnit[loadedItems[i].code] = loadedItems[i];
        }

        SpecialUnit[] specialloadedItems = Resources.LoadAll<SpecialUnit>("Asset/Unit/SpecialUnit");
        for (int i = 0; i < specialloadedItems.Length; i++)
        {
            specialUnit[specialloadedItems[i].code] = specialloadedItems[i];
        }
    }

    public Unit_Scriptable GetUnit(string code)
    {
        if (code[0] == 'S')
        {
            return specialUnit[code];
        }
        else
        {
            return randomUnit[code];
        }
    }
}
