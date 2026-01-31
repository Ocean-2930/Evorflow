using System.Collections.Generic;
using UnityEngine;

public class BenchTable : CustomMouseInterface
{
    [SerializeField] private GameObject partyTable = null;
    [SerializeField] private GameObject cardHolder = null;
    [SerializeField] private GameObject unitCard = null;
    [SerializeField] private GameObject statPopup = null;
    [SerializeField] private float cardPadding = 75.0f;

    private Table benchTableData = new Table();
    private Table partyTableData = new Table();
    private List<GameObject> cards = new List<GameObject>();

    private float cardSize { get { return unitCard.GetComponent<RectTransform>().sizeDelta.x; } }
    private bool openParty = true;
    private bool popup = false;

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

    public void AddCard(Unit unit)
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

        benchTableData.AddUnit(obj.GetComponent<UnitCard>().unit);
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

    public void SwitchParty(GameObject obj)
    {
        if (!openParty) { return; }
        SwitchParty(obj.GetComponent<UnitCard>().unit);
    }

    public void SwitchParty(Unit tunit)
    {
        if (!openParty) { return; }
        if (benchTableData.Contains(tunit) && !partyTableData.Contains(tunit))
        {
            partyTable.GetComponent<PartyTable>().AddUnit(tunit);
            benchTableData.Remove(tunit);
            partyTableData.Add(tunit);
        }
        else if (!benchTableData.Contains(tunit) && partyTableData.Contains(tunit))
        {
            partyTable.GetComponent<PartyTable>().RemoveUnit(tunit);
            benchTableData.Add(tunit);
            partyTableData.Remove(tunit);
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

    public void CleanBench()
    {
        for (int i = benchTableData.Count - 1; 0 <= i; i--)
        {
            SwitchParty(benchTableData[i]);
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

    public override void OnEnter()
    {
        GameObject obj = Instantiate(statPopup, Vector3.zero, Quaternion.identity);
        obj.GetComponent<StatusField>().UpdateStatus(benchTableData);
        MousePopup.inst.ViewPopup(obj);
    }

    public override void OnExit()
    {
        MousePopup.inst.CleanPopup();
    }

    public override void OnLeftClick()
    {
        float cx = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(cx, (popup ? -720 : -280), 0);
        partyTable.SetActive(!popup && openParty);
        popup = !popup;
    }
}
