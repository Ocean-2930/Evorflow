using System.Collections;
using UnityEngine;

public class ScenarioControl : SceneSingleton<ScenarioControl>
{
    [SerializeField] private GameObject scenarioText;

    private ScenarioText _scenarioText;
    private ScenarioText stext
    {
        get
        {
            if (_scenarioText != null)
            {
                return _scenarioText;
            }

            if (scenarioText != null)
            {
                _scenarioText = scenarioText.GetComponent<ScenarioText>();
            }

            if (_scenarioText == null)
            {
                _scenarioText = FindFirstObjectByType<ScenarioText>();
            }

            return _scenarioText;
        }
    }

    public override string className => "ScenarioControl";

    private Coroutine scenarioLoop = null;
    private int optionPipe = -1;
    private bool partyUpdated = false;

    private void OnEnable()
    {
        if (stext != null)
        {
            stext.PartyChanged += OnPartyChanged;
        }
    }

    private void OnDisable()
    {
        if (_scenarioText != null)
        {
            _scenarioText.PartyChanged -= OnPartyChanged;
        }
    }

    private void OnPartyChanged()
    {
        partyUpdated = true;
    }

    public void StartScenario(Scenario first)
    {
        if (scenarioLoop == null)
        {
            scenarioLoop = StartCoroutine(ScenarioLoop(first));
        }
    }

    private void UserOption(int option)
    {
        optionPipe = option;
    }

    private IEnumerator ScenarioLoop(Scenario first)
    {
        ScenarioFlow sflow = new ScenarioFlow(first);

        while (!sflow.scenarioEnd)
        {
            IEnumerator enumerator = sflow.GetEnumerator();
            yield return enumerator;
            sflow.SafeProceed();
        }

        scenarioLoop = null;
    }

    public IEnumerator ScenarioGeneral(ScenarioFlow sflow)
    {
        Scenario_Scriptable scenario = (Scenario_Scriptable)sflow.currentScenario;

        stext.AddTextBox(scenario.GetScript());
        optionPipe = -1;

        string[] options = sflow.GetOptions();
        stext.OpenOptions(options, UserOption);

        yield return new WaitUntil(() => optionPipe != -1);

        stext.CleanOptions();
        sflow.Proceed(((options.Length != 0) ? options[optionPipe] : ""));
        yield return null;
    }

    public IEnumerator ScenarioTable(ScenarioFlow sflow)
    {
        stext.AddTextBox(sflow.currentScenario.GetScript());
        optionPipe = -1;

        stext.OpenTable();

        while (true)
        {
            stext.CleanOptions();
            string[] options = sflow.GetOptions(stext.partyTableData);
            stext.OpenOptions(options, UserOption);

            yield return new WaitUntil(() => partyUpdated || optionPipe != -1);

            partyUpdated = false;
            if (optionPipe != -1)
            {
                sflow.Proceed(options[optionPipe]);
                break;
            }
        }

        stext.CleanOptions();
        stext.CloseTable();

        yield return null;
    }
}
