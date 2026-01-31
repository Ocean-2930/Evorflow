using System;
using UnityEngine;

[System.Serializable]
public class EnumIntArray<TEnum> where TEnum : Enum
{
    [SerializeField] private int[] array = new int[Enum.GetValues(typeof(TEnum)).Length];

    public int this[TEnum e]
    {
        get => array[Convert.ToInt32(e)];
        set => array[Convert.ToInt32(e)] = value;
    }

    public int this[int ind]
    {
        get
        {
            return array[ind];
        }
        set
        {
            array[ind] = value;
        }
    }

    public int length
    {
        get
        {
            return Enum.GetValues(typeof(TEnum)).Length;
        }
    }

    public static EnumIntArray<TEnum> operator +(EnumIntArray<TEnum> a, EnumIntArray<TEnum> b)
    {
        EnumIntArray<TEnum> rarr = new EnumIntArray<TEnum>();

        for(int i = 0; i< Enum.GetValues(typeof(TEnum)).Length; i++)
        {
            rarr[i] = a[i] + b[i];
        }

        return rarr;
    }

    public int Sum()
    {
        int sum = 0;

        for(int i = 0; i<array.Length;i++)
        {
            sum += array[i];
        }

        return sum;
    }
}