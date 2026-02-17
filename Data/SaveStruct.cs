using UnityEngine;

public struct ItemData
{
    public string code;
    public int amount;
}

public struct UnitData
{
    public string code;

    public int MAXHP;
    public int HP;
    public int MAXCON;
    public int CON;
    public int STR;
    public int INT;
    public int AGI;
    public int VIT;

    public string baseSkill;
    public string skill1;
    public string skill2;
    public string skill3;
}

public class SaveStruct : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
