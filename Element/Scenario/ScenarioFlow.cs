using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScenarioFlow
{
    public ScenarioFlow(Scenario first)
    {
        pipeline.AddFirst(first);
        pipelineFlag = pipeline.First;
    }

    #region ScenarioControl
    public bool scenarioEnd
    {
        get
        {
            return pipeline.Count == 0;
        }
    }

    public ScenarioType scenarioType
    {
        get
        {
            return currentScenario.scenarioType;
        }
    }

    public Scenario currentScenario { get { return pipeline.First.Value; } }

    public IEnumerator GetEnumerator()
    {
        return currentScenario.GetEnumerator(this);
    }

    public string[] GetOptions()
    {
        givenOptions = new string[0];

        Scenario_Scriptable sc = (Scenario_Scriptable)currentScenario;
        sc.GetOptions(this);
        return givenOptions;
    }

    public string[] GetOptions(Table t)
    {
        userTable = t;
        return GetOptions();
    }

    private bool proceeded = false;
    public void NextScenario()
    {
        proceeded = true;

        pipeline.RemoveFirst();
        pipelineFlag = pipeline.First;
    }

    public void Proceed(string option)
    {
        Scenario_Scriptable sc = (Scenario_Scriptable)currentScenario;
        userOption = option;

        sc.ChoseOption(this);

        NextScenario();
    }

    public void SafeProceed()
    {
        if (!proceeded)
        {
            NextScenario();
        }

        proceeded = false;
    }
    #endregion

    #region Scenario
    public string[] givenOptions;
    public string userOption;
    public Table userTable;

    private List<string> items = new List<string>();
    private List<string> tags = new List<string>();


    public void AddTag(string newTag)
    {
        tags.Add(newTag);
    }
    #endregion

    #region Scenario Pipeline
    private LinkedList<Scenario> pipeline = new LinkedList<Scenario>();
    private LinkedListNode<Scenario> pipelineFlag;

    public void Pipeline(Scenario newScenario)
    {
        if (newScenario == null) { return; }
        pipeline.AddAfter(pipelineFlag, newScenario);
        pipelineFlag = pipelineFlag.Next;
    }
    #endregion
}
