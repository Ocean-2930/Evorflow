using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCloud : Singleton<ScenarioCloud>
{
    public override string className { get { return "ScenarioCloud"; } }

    public Dictionary<string, Scenario> scenarioList = new Dictionary<string, Scenario>();

    private void Awake()
    {
        CSVParser parser = new CSVParser("Text/4_Expedition/scenario");
        for (int i = 1; i < parser.data.Length; i++)
        {
            string line = parser.data[i];
            if (line == "") { continue; }
            int ind = line.IndexOf(',');
            scenarioList.Add(line.Substring(0, ind), new Scenario(parser.data[i]));
        }
    }
}

public class Scenario
{
    public enum ScenarioType
    {
        option = 0,
        party = 1,
        items = 2,
        end = 3
    }

    private class LanguagePack
    {
        public string scenarioName;
        public string scenarioText;
        public string[] optionText;

        public LanguagePack(string content)
        {
            string[] sLine = content.Split('#');
            scenarioName = sLine[0];
            scenarioText = sLine[1];
            optionText = new string[sLine.Length - 2];
            for (int i = 0; i < optionText.Length; i++)
            {
                optionText[i] = sLine[i + 2];
            }
        }
    }

    private class ScenarioOption
    {
        private class ScenarioFunction
        {
            private Func<ScenarioFlow, Scenario> function;
            private string functionInput;

            public ScenarioFunction(string inText)
            {
                string[] sText = inText.Split(":");
                function = GetFunc(sText[0]);
                if (2 <= sText.Length)
                {
                    functionInput = sText[1];
                }
            }

            private Func<ScenarioFlow, Scenario> GetFunc(string name)
            {
                switch(name)
                {
                    case "to": return _To;
                    case "addtag": return _AddTag;
                    case "pipeline": return _Pipeline;
                    case "end": return _End;
                    default: return null;
                }
            }

            public Scenario Activate(ScenarioFlow sFlow)
            {
                return function.Invoke(sFlow);
            }

            private Scenario _To(ScenarioFlow sFlow)
            {
                return ScenarioCloud.inst.scenarioList[functionInput];
            }

            private Scenario _AddTag(ScenarioFlow sFlow)
            {
                sFlow.AddTag(functionInput);
                return null;
            }

            private Scenario _Pipeline(ScenarioFlow sFlow)
            {
                sFlow.Pipeline(ScenarioCloud.inst.scenarioList[functionInput]);
                return null;
            }

            private Scenario _End(ScenarioFlow sFlow)
            {
                return null;
            }
        }

        private ScenarioFunction[] functions;

        public ScenarioOption(string line)
        {
            string[] sLine = line.Split(">");
            functions = new ScenarioFunction[sLine.Length];
            for(int i = 0; i< sLine.Length;i++)
            {
                functions[i] = new ScenarioFunction(sLine[i]);
            }
        }

        public Scenario Activate(ScenarioFlow flow)
        {
            Scenario rScenario = null;
            for (int i = 0; i < functions.Length; i++)
            {
                rScenario = functions[i].Activate(flow);
            }
            return rScenario;
        }
    }

    private LanguagePack userPack { get { return languagePacks[(int)UserOption.language]; } }
    private LanguagePack[] languagePacks = new LanguagePack[(int)Language.None];    
    private ScenarioOption[] scenarioOptions;
    private ScenarioType scenarioType = ScenarioType.option;

    public Scenario(string line)
    {
        switch(line[0])
        {
            case 'O':
                scenarioType = ScenarioType.option;
                break;
            case 'P':
                scenarioType = ScenarioType.party;
                break;
            case 'I':
                scenarioType = ScenarioType.items;
                break;
            default:
                break;
        }

        string[] sLine = line.Split(',');
        
        for (int i = 0; i < languagePacks.Length; i++)
        {
            languagePacks[i] = new LanguagePack(sLine[i + 1]);
        }
        
        if (sLine[1 + languagePacks.Length] != "")
        {
            string[] oLine = sLine[1 + languagePacks.Length].Split("#");
            scenarioOptions = new ScenarioOption[oLine.Length];
            for (int i = 0; i < oLine.Length; i++)
            {
                scenarioOptions[i] = new ScenarioOption(oLine[i]);
            }
        }
    }

    public Scenario Choice(ScenarioFlow flow, int index) { return scenarioOptions[index].Activate(flow); }

    public string scenarioName { get { return userPack.scenarioName; } }
    public string scenarioText { get { return userPack.scenarioText; } }
    public int optionCount { get { return scenarioOptions.Length; } }
    public string[] optionText { get { return userPack.optionText; } }
    public ScenarioType type { get { return scenarioType; } }
}

