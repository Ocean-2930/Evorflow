using System.Collections.Generic;
using UnityEngine;

public class UnitCloud : Singleton<UnitCloud>
{
    public override string className { get { return "UnitCloud"; } }

    public Dictionary<string, UnitBase> randomUnit = new Dictionary<string, UnitBase>();
    public Dictionary<string, SpecialUnit> specialUnit = new Dictionary<string, SpecialUnit>();

    private void Awake()
    {
        UnitBase[] loadedItems = Resources.LoadAll<UnitBase>("Asset/Unit/RandomUnit");
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

    public UnitBase GetUnit(string code)
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
