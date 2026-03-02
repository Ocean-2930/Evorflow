using System.Collections.Generic;

public enum DamageType
{
    Damage = 0,
    Heal = 1,
    Buff = 2
}

public enum DamageBoxCalculation
{
    UserPlus = 0,
    UserMinus = 1,
    Multiply = 2,
    ReceivePlue = 3,
    ReceiveMinus = 4,
    SetTo = 5,
    None = 6
}

public class DamageBox
{
    private float _value;
    private List<float>[] calculations;

    public int value
    {
        get
        {
            float buff = _value;
            for (int i = 0; i < calculations.Length; i++)
            {
                for (int j = 0; j < calculations[i].Count; j++)
                {
                    switch (i)
                    {
                        case (int)DamageBoxCalculation.UserPlus:
                            buff += calculations[i][j];
                            break;
                        case (int)DamageBoxCalculation.UserMinus:
                            buff -= calculations[i][j];
                            if (buff < 0)
                            {
                                buff = 0;
                            }
                            break;
                        case (int)DamageBoxCalculation.Multiply:
                            buff *= calculations[i][j];
                            break;
                        case (int)DamageBoxCalculation.ReceivePlue:
                            buff += calculations[i][j];
                            break;
                        case (int)DamageBoxCalculation.ReceiveMinus:
                            buff -= calculations[i][j];
                            if (buff < 0)
                            {
                                buff = 0;
                            }
                            break;
                        case (int)DamageBoxCalculation.SetTo:
                            buff = calculations[i][j];
                            break;
                        default:
                            break;
                    }
                }
            }

            return (int)buff;
        }
    }

    public DamageType damageType;

    public DamageBox(DamageType dtype, int amount)
    {
        _value = (float)amount;
        damageType = dtype;
        calculations = new List<float>[(int)DamageBoxCalculation.None];
        for (int i = 0; i < calculations.Length; i++)
        {
            calculations[i] = new List<float>();
        }
    }

    public void AddCalculation(DamageBoxCalculation calc, int amount)
    {
        AddCalculation(calc, (float)amount);
    }

    public void AddCalculation(DamageBoxCalculation calc, float amount)
    {
        int ind = (int)calc;

        if ((int)DamageBoxCalculation.None <= ind)
        {
            return;
        }

        calculations[ind].Add(amount);
    }
}
