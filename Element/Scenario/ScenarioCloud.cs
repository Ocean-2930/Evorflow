using System.Collections.Generic;
using UnityEngine;

public class ScenarioCloud : Singleton<ScenarioCloud>
{
    public override string className { get { return "ScenarioCloud"; } }

    public Scenario_Battle scenarioBattle;
    public Scenario_Item scenarioItem;
    public Scenario_Death scenarioDeath;
    public Scenario_Cinema scenarioCinema;
    public Scenario_Close scenarioClose;

    public Dictionary<string, Scenario> scenarioList = new Dictionary<string, Scenario>();
    public Dictionary<string, Scenario> subscenarioList = new Dictionary<string, Scenario>();

    private void Awake()
    {
        scenarioBattle = ScriptableObject.CreateInstance<Scenario_Battle>();
        scenarioItem = ScriptableObject.CreateInstance<Scenario_Item>();
        scenarioDeath = ScriptableObject.CreateInstance<Scenario_Death>();
        scenarioCinema = ScriptableObject.CreateInstance<Scenario_Cinema>();
        scenarioClose = ScriptableObject.CreateInstance<Scenario_Close>();

        Scenario[] loadedItems = Resources.LoadAll<Scenario>("Asset/Scenario/NodeScenario");
        for (int i = 0; i < loadedItems.Length; i++)
        {
            scenarioList.Add(loadedItems[i].scenarioCode, loadedItems[i]);
        }

        string filename = "scenario";
        CSVParser parser = new CSVParser("Text/4_Expedition/" + filename);
        for (int i = 0; i < parser.data.Length; i++)
        {
            string line = parser.data[i];
            if (line == "") { continue; }
            int ind = line.IndexOf(',');
            string key = line.Substring(0, ind);

            if (!scenarioList.ContainsKey(key))
            {
                Debug.Log($"!!!EventCode: {key} from {filename} CSV not found in Scriptable Objects!!!");
                continue;
            }

            scenarioList[key].AddTextData(line.Substring(ind + 1));
        }
    }
}
