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

public class ScenarioText : MonoBehaviour
{
    [SerializeField] private GameObject optionPin;
    private RectTransform _optionTransform;
    private RectTransform optionTransform
    {
        get
        {
            if(_optionTransform != null)
            {
                return _optionTransform;
            }

            _optionTransform = optionPin.GetComponent<RectTransform>();
            return _optionTransform;
        }
    }

    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject optionBox;
    [SerializeField] private GameObject scenarioTable;
    [SerializeField] private float optionSpacing = 140.0f;
    private Coroutine scenarioLoop = null;
    private int optionPipe = -1;

    private GameObject openTable;

    private CustomScrollUI_Manual _scrollUI;
    private CustomScrollUI_Manual scrollUI
    {
        get
        {
            if (_scrollUI != null)
            {
                return _scrollUI;
            }
            
            _scrollUI = gameObject.GetComponent<CustomScrollUI_Manual>();
            return _scrollUI;
        }
    }

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
        ScenarioFlow sflow = new ScenarioFlow(first);

        while (!sflow.scenarioEnd)
        {
            switch(sflow.scenarioType)
            {
                case ScenarioType.General:
                    yield return ScenarioGeneral(sflow);
                    break;
                case ScenarioType.Table:
                    yield return ScenarioTable(sflow);
                    break;
                default:
                    break;
            }
        }
        
        scenarioLoop = null;
    }

    private IEnumerator ScenarioGeneral(ScenarioFlow sflow)
    {
        Scenario_General scenario = (Scenario_General)sflow.currentScenario;

        AddTextBox(scenario.GetScript());
        optionPipe = -1;

        string[] options = scenario.GetOptions(sflow);
        OpenOptions(options);

        yield return new WaitUntil(() => optionPipe != -1);
        
        CleanOptions();
        sflow.Proceed(((options.Length != 0) ? options[optionPipe] : ""));
    }

    private IEnumerator ScenarioTable(ScenarioFlow sflow)
    {
        Scenario_Table scenario = (Scenario_Table)sflow.currentScenario;

        AddTextBox(scenario.GetScript());
        optionPipe = -1;

        openTable = Instantiate(scenarioTable, Vector3.zero, Quaternion.identity);        
        scrollUI.AddContent(openTable);


        //link table
        /*
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
        */
        yield return null;
    }

    private void OpenOptions(string[] inoptions)
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
            rect.SetParent(optionTransform);
            rect.anchoredPosition = new Vector3(0, ypos, 0);
            ypos -= optionSpacing;
        }
    }

    private void CleanOptions()
    {
        for (int i = optionTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(optionTransform.GetChild(i).gameObject);
        }
    }

    public void AddTextBox(string instr)
    {
        GameObject obj = Instantiate(textBox, Vector3.zero, Quaternion.identity);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector3(scrollUI.windowTransform.rect.width, rect.sizeDelta.y, 0);
        obj.GetComponent<TextBox>().UpdateText(instr);
        scrollUI.AddContent(obj);
        scrollUI.ToBottom();
    }
}
