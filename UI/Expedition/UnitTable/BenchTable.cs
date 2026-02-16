using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BenchTable : MonoBehaviour, ICustomMouseInterface
{
    [SerializeField] private GameObject cardHolder = null;
    [SerializeField] private GameObject unitCard = null;
    [SerializeField] private GameObject statPopup = null;
    [SerializeField] private GameObject scenarioText;
    private ScenarioText _sText;
    private ScenarioText sText
    {
        get
        {
            if (_sText != null)
            {
                return _sText;
            }
            _sText = scenarioText.GetComponent<ScenarioText>();
            return _sText;
        }
    }

    [SerializeField] private float cardPadding = 75.0f;

    private Table benchTableData = new Table();
    private List<GameObject> cards = new List<GameObject>();

    private float cardSize { get { return unitCard.GetComponent<RectTransform>().sizeDelta.x; } }

    void Start()
    {
        int i = 0;
        while (i < 5)
        {
            if (ExpeditonInven.inst.units[i] == null)
            {
                break;
            }

            AddCard(ExpeditonInven.inst.units[i]);
            i++;
        }
    }

    public void AddCard(UnitInst unit)
    {
        if (4 < cards.Count)
        {
            return;
        }

        GameObject obj = Instantiate(unitCard, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(cardHolder.transform);
        obj.GetComponent<UnitCard>().SetUnit(unit);
        cards.Add(obj);
        ArrangeCard();

        benchTableData.Add(obj.GetComponent<UnitCard>().unit);
    }

    public void RemoveCard(GameObject obj)
    {
        if (!cards.Contains(obj))
        {
            return;
        }

        benchTableData.Remove(obj.GetComponent<UnitCard>().unit);
        cards.Remove(obj);
        ArrangeCard();
    }

    public void SwitchParty(UnitInst tunit)
    {
        if (!sText.tableOpened) { return; }

        if (benchTableData.Contains(tunit))
        {
            benchTableData.Remove(tunit);
            sText.AddParty(tunit);
        }
        else if (!benchTableData.Contains(tunit))
        {
            benchTableData.Add(tunit);
            sText.RemoveParty(tunit);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<UnitCard_Scenario>().unit == tunit)
            {
                cards[i].GetComponent<UnitCard_Scenario>().ToggleCard();
                break;
            }
        }
    }

    public void CleanTable()
    {
        benchTableData.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            UnitCard_Scenario c = cards[i].GetComponent<UnitCard_Scenario>();
            benchTableData.Add(c.unit);
            c.ToBench();
        }
    }

    protected void ArrangeCard()
    {
        int cardCnt = cards.Count;
        float xlen = cardSize;

        for (int i = 0; i < cardCnt; i++)
        {
            float ind = i - cardCnt / 2.0f + 0.5f;
            float xpos = ind * (xlen + cardPadding);
            cards[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(xpos, 0, 0);
        }
    }

    public void OnEnter()
    {
        GameObject obj = Instantiate(statPopup, Vector3.zero, Quaternion.identity);
        obj.GetComponent<StatusField>().UpdateStatus(benchTableData);
        MousePopup.inst.ViewPopup(obj);
    }

    public void OnExit()
    {
        MousePopup.inst.CleanPopup();
    }
}
