using System.Collections.Generic;
using UnityEngine;

public class PunchTest : MonoBehaviour
{
    public static PunchTest obj;
    public bool button = false;

    public string instr;
    public bool punch = false;

    void Start()
    {
        if(!button)
        {
            obj = this;
        }
    }

    void Update()
    {
        if(punch)
        {
            punch = false;
            gameObject.GetComponent<ScenarioText>().StartScenario(ScenarioCloud.inst.scenarioList["001"]);
            TryFunc();            
        }
    }

    public void Punch()
    {
        obj.punch = true;
    }

    private void ParserTest()
    {
        CSVParser parser = new CSVParser("Text/opening/opening");
        foreach(string line in parser.data)
        {
            Debug.Log(line);
        }
    }

    private void TryFunc()
    {
        
    }
}
