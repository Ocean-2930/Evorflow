using UnityEngine;

[CreateAssetMenu(fileName = "Scenario_003", menuName = "Scriptable Objects/Scenario/NodeScenario/Scenario_003", order=3)]
public class Scenario_003 : Scenario_General
{
    public override string[] GetOptions(ScenarioFlow flow)
    {
        return new string[] {  };
    }

    public override void ChoseOption(ScenarioFlow flow, string option)
    {

    }
}
