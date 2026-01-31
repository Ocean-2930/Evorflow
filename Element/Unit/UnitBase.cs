using UnityEngine;

[CreateAssetMenu(fileName = "UnitBase", menuName = "Scriptable Objects/Unit/RandomUnit")]
public class UnitBase : ScriptableObject
{
    public string code;
    public Sprite illust;
    public EnumIntArray<StatType> baseStat = new EnumIntArray<StatType>();
}
