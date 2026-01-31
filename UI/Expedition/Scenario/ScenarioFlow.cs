using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScenarioFlow
{
    private LinkedList<Scenario> pipeline = new LinkedList<Scenario>();
    private LinkedListNode<Scenario> pipelineFlag;
    private List<string> tags = new List<string>();
    private List<string> items = new List<string>();

    public ScenarioFlow(Scenario first)
    {
        pipeline.AddFirst(first);
        pipelineFlag = pipeline.First;
    }

    public bool scenarioEnd { get { return pipeline.Count == 0; } }

    public Scenario currentScenario { get { return pipeline.First.Value; } }

    public Scenario.ScenarioType scenarioType
    {
        get
        {
            if (pipeline.First.Value == null)
            {
                return Scenario.ScenarioType.option;
            }

            return pipeline.First.Value.type;
        }
    }

    public void Proceed(int option)
    {
        Pipeline(currentScenario.Choice(this, option));
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
