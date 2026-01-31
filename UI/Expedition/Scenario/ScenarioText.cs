using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public enum Language
{
    Korean = 0,
    English = 1,
    None = 2
}

public class ScenarioText : CustomScrollUI_Manual
{
    [SerializeField] private GameObject optionPin;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject optionBox;
    [SerializeField] private GameObject scenarioTable;
    [SerializeField] private float optionSpacing = 140.0f;
    private Coroutine scenarioLoop = null;
    private int optionPipe = -1;

    private GameObject openTable;
    private bool userUpdate = false;

    public void StartScenario(Scenario first)
    {
        if (scenarioLoop == null) { scenarioLoop = StartCoroutine(ScenarioLoop(first)); }
    }

    public void Option(int option)
    {
        optionPipe = option;
    }

    private IEnumerator ScenarioLoop(Scenario first)
    {
        RectTransform opTransform = optionPin.GetComponent<RectTransform>();
        ScenarioFlow sflow = new ScenarioFlow(first);

        while (!sflow.scenarioEnd)
        {
            switch(sflow.scenarioType)
            {
                case Scenario.ScenarioType.option:
                    yield return ScenarioOption(opTransform, sflow);
                    break;
                case Scenario.ScenarioType.party:
                    yield return ScenarioParty(opTransform, sflow);
                    break;
                default:
                    break;
            }
        }
        
        scenarioLoop = null;
    }

    private IEnumerator ScenarioOption(RectTransform opTransform, ScenarioFlow sflow)
    {
        AddTextBox(sflow.currentScenario.scenarioText);
        optionPipe = -1;

        OpenOptions(opTransform, sflow.currentScenario.optionText);

        yield return new WaitUntil(() => optionPipe != -1);
        CleanOptions(opTransform);
        sflow.Proceed(optionPipe);
    }

    private IEnumerator ScenarioParty(RectTransform opTransform, ScenarioFlow sflow)
    {
        AddTextBox(sflow.currentScenario.scenarioText);
        optionPipe = -1;

        openTable = Instantiate(scenarioTable, Vector3.zero, Quaternion.identity);        
        AddContent(openTable);
        //link table

        ScenarioTable sTable = openTable.GetComponent<ScenarioTable>();
        while (true)
        {
            yield return new WaitUntil(() => optionPipe != -1 || userUpdate);

            if(optionPipe != -1)
            {
                break;
            }

            if(userUpdate)
            {
                if(sTable.tableData.Count == 0)
                {
                    CleanOptions(opTransform);
                }
                else
                {
                    OpenOptions(opTransform, sflow.currentScenario.optionText);
                }
            }
        }


        //cut connection
        sflow.Proceed(optionPipe);
    }

    private void OpenOptions(RectTransform opTransform, string[] inoptions)
    {
        string[] options;
        if (inoptions.Length == 0) { options = new string[1]; options[0] = "No Text"; }
        else { options = inoptions; }

        float ypos = 0.0f;
        for (int i = 0; i < options.Length; i++)
        {
            string optionText = options[i];
            GameObject obj = Instantiate(optionBox);
            obj.GetComponent<OptionBox>().Initialize(this, i, optionText);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.SetParent(opTransform);
            rect.anchoredPosition = new Vector3(0, ypos, 0);
            ypos -= optionSpacing;
        }
    }

    private void CleanOptions(RectTransform opTransform)
    {
        for (int i = opTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(opTransform.GetChild(i).gameObject);
        }
    }

    public void AddTextBox(string instr)
    {
        GameObject obj = Instantiate(textBox, Vector3.zero, Quaternion.identity);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector3(windowTransform.rect.width, rect.sizeDelta.y, 0);
        obj.GetComponent<TextBox>().UpdateText(instr);
        AddContent(obj);
        ToBottom();
    }
}
