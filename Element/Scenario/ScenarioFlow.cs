using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScenarioFlow
{
    private LinkedList<Scenario> pipeline = new LinkedList<Scenario>();
    private LinkedListNode<Scenario> pipelineFlag;
    private List<string> tags = new List<string>();

    private Table table;
    private List<string> items = new List<string>();

    public ScenarioFlow(Scenario first)
    {
        pipeline.AddFirst(first);
        pipelineFlag = pipeline.First;
    }

    public bool scenarioEnd { get { return pipeline.Count == 0; } }

    public Scenario currentScenario { get { return pipeline.First.Value; } }

    public ScenarioType scenarioType
    {
        get
        {
            return currentScenario.scenarioType;
        }
    }

    public void ConnectTable(Table t)
    {
        table = t;
    }

    public void Proceed()
    {
        Proceed("");
    }

    public void Proceed(string option)
    {
        switch (currentScenario.scenarioType)
        {
            case ScenarioType.General:
                ((Scenario_General)currentScenario).ChoseOption(this, option);
                break;
            case ScenarioType.Table:
                ((Scenario_Table)currentScenario).ChoseOption(this, table, option);
                break;
            default:
                break;
        }

        pipeline.RemoveFirst();
        pipelineFlag = pipeline.First;
    }

    public void AddTag(string newTag)
    {
        tags.Add(newTag);
    }

    public void Pipeline(Scenario newScenario)
    {
        if (newScenario == null) { return; }
        pipeline.AddAfter(pipelineFlag, newScenario);
        pipelineFlag = pipelineFlag.Next;
    }
}
