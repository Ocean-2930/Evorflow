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
    }

    public void Scenario_002_ChoseOption(ScenarioFlow sflow)
    {
        Scenario scenario = sflow.currentScenario;
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
