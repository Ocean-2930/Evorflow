using UnityEngine;

[CreateAssetMenu(fileName = "Scenario_001", menuName = "Scriptable Objects/Scenario/NodeScenario/Scenario_001", order=1)]
public class Scenario_001 : Scenario_General
{
    public override string[] GetOptions(ScenarioFlow flow)
    {
        string[] t = textpack;
        return new string[] { t[1], t[2], t[3] };
    }

    public override void ChoseOption(ScenarioFlow flow, string option)
    {
        if (option == textpack[1])
        {
            flow.Pipeline(ScenarioCloud.inst.scenarioList["002"]);
        }
        else if (option == textpack[2])
        {
            flow.Pipeline(ScenarioCloud.inst.scenarioList["003"]);
        }
        else if (option == textpack[3])
        {
            flow.Pipeline(ScenarioCloud.inst.scenarioList["002"]);
            flow.Pipeline(ScenarioCloud.inst.scenarioList["003"]);
        }
    }
}
