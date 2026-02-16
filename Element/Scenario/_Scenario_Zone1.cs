using UnityEngine;

public class _Scenario_Zone1 : MonoBehaviour
{
    public void Scenario_001_GetOptions(ScenarioFlow sflow)
    {
        Scenario scenario = sflow.currentScenario;
        sflow.givenOptions = new string[] { scenario.textpack[1], scenario.textpack[2], scenario.textpack[3] };
    }

    public void Scenario_001_ChoseOption(ScenarioFlow sflow)
    {
        Scenario scenario = sflow.currentScenario;
        if (sflow.userOption == scenario.textpack[1])
        {
            sflow.Pipeline(ScenarioCloud.inst.scenarioList["002"]);
        }
        else if (sflow.userOption == scenario.textpack[2])
        {
            sflow.Pipeline(ScenarioCloud.inst.scenarioList["003"]);
        }
        else if (sflow.userOption == scenario.textpack[3])
        {
            sflow.Pipeline(ScenarioCloud.inst.scenarioList["002"]);
            sflow.Pipeline(ScenarioCloud.inst.scenarioList["003"]);
        }
    }

    public void Scenario_002_GetOptions(ScenarioFlow sflow)
    {
        Scenario scenario = sflow.currentScenario;
        if(sflow.userTable.Count >= 3)
        {
            sflow.givenOptions = new string[] { scenario.textpack[1], scenario.textpack[2] };
        }
        else
        {
            sflow.givenOptions = new string[] { scenario.textpack[2] };
        }
    }

    public void Scenario_002_ChoseOption(ScenarioFlow sflow)
    {
        Scenario scenario = sflow.currentScenario;
        if (sflow.userOption == scenario.textpack[1])
        {
            Debug.Log("문을 열고 아이템 획득");
        }
        else
        {
            Debug.Log("차량은 내버려두고 물러났다.");
        }
    }
}

/* new scenario template
public void Scenario_001_GetOptions(ScenarioFlow sflow)
{
    Scenario scenario = sflow.currentScenario;
}

public void Scenario_001_ChoseOption(ScenarioFlow sflow)
{
    Scenario scenario = sflow.currentScenario;
}
*/
