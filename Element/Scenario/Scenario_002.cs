using UnityEngine;

[CreateAssetMenu(fileName = "Scenario_002", menuName = "Scriptable Objects/Scenario/NodeScenario/Scenario_002", order=2)]
public class Scenario_002 : Scenario_General
{
    public override string[] GetOptions(ScenarioFlow flow)
    {
        return new string[] { };
    }

    public override void ChoseOption(ScenarioFlow flow, string option)
    {
        
    }
}
