using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit/RandomUnit")]
public class Unit_Scriptable : ScriptableObject
{
    public string code;
    public Sprite illust;
    public EnumIntArray<StatType> baseStat = new EnumIntArray<StatType>();
}
