using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScenarioEvent : UnityEvent<ScenarioFlow> { }

[CreateAssetMenu(fileName = "Scenario", menuName = "Scriptable Objects/Scenario")]
public class Scenario_Scriptable : Scenario
{
    [SerializeField] private string _scenarioCode;
    public override string scenarioCode => _scenarioCode;

    public enum IsNode
    {
        Node = 0,
        Sub = 1
    }
    [SerializeField] private IsNode _isNode = IsNode.Node;
    public IsNode isNode { get { return _isNode; } }

    private enum ScenarioInput
    {
        General = 0,
        Table = 1
    }
    [SerializeField] private ScenarioInput type = ScenarioInput.General;
    public override ScenarioType scenarioType => (type == ScenarioInput.General) ? ScenarioType.General : ScenarioType.Table;


    public override IEnumerator GetEnumerator(ScenarioFlow flow)
    {
        return (type == ScenarioInput.General) ? ScenarioControl.inst.ScenarioGeneral(flow) : ScenarioControl.inst.ScenarioTable(flow);
    }

    [SerializeField] private ScenarioEvent _GetOptions;
    [SerializeField] private ScenarioEvent _ChoseOption;

    public void GetOptions(ScenarioFlow flow)
    {
        _GetOptions?.Invoke(flow);
    }

    public void ChoseOption(ScenarioFlow flow)
    {
        _ChoseOption?.Invoke(flow);
    }
}
