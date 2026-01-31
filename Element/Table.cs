using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Table : List<Unit>
{
    public int unitcnt
    {
        get
        {
            return Count;
        }
    }

    public int stren
    {
        get
        {
            if(unitcnt == 0)
            {
                return 0;
            }

            int buff = this[0].stat[StatType.STR];

            for (int i = 1; i < unitcnt; i++)
            {
                buff += this[i].stat[StatType.STR];
            }

            return buff;
        }
    }

    public int intel
    {
        get
        {
            if (unitcnt == 0)
            {
                return 0;
            }

            int buff = this[0].stat[StatType.INT];

            for (int i = 1; i < unitcnt; i++)
            {
                if (buff < this[i].stat[StatType.INT])
                {
                    buff = this[i].stat[StatType.INT];
                }
            }

            return buff;
        }
    }

    public int agil
    {
        get
        {
            if (unitcnt == 0)
            {
                return 0;
            }

            int buff = this[0].stat[StatType.AGI];

            for (int i = 0; i < unitcnt; i++)
            {
                if (this[i].stat[StatType.AGI] < buff)
                {
                    buff = this[i].stat[StatType.AGI];
                }
            }

            return buff;
        }
    }

    public void AddUnit(Unit unit)
    {
        unit.table = this;
        Add(unit);
    }
}
