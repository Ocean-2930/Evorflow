using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.GPUSort;

public partial class PartyTable : TableUI, ICustomMouseInterface
{
    [SerializeField] private GameObject benchTable;
    [SerializeField] private GameObject statPopup;

    private void Awake()
    {
        ICustomMouseInterface k = this;
        k.OpenInterface(gameObject);
    }

    public override bool AddUnit(Unit inUnit)
    {
        if (FindToken(inUnit) != null) { return false; }

        GameObject newtoken = Instantiate(unitToken, Vector3.zero, Quaternion.identity, transform);
        tokens.Add(newtoken);
        tableData.Add(inUnit);
        newtoken.GetComponent<PartyToken>().Initialize(benchTable, inUnit);
        ArrangeTokens();

        return true;
    }

    public void OnEnter()
    {
        GameObject obj = Instantiate(statPopup, Vector3.zero, Quaternion.identity);
        obj.GetComponent<StatusField>().UpdateStatus(tableData);
        MousePopup.inst.ViewPopup(obj);
    }

    public void OnExit()
    {
        MousePopup.inst.CleanPopup();
    }
}

public class TableUI : MonoBehaviour
{
    [SerializeField] protected GameObject unitToken;
    [SerializeField] protected float padding = 30.0f;

    protected float tokenSize { get { return unitToken.GetComponent<RectTransform>().sizeDelta.x; } }
    protected List<GameObject> tokens = new List<GameObject>();
    public Table tableData = new Table();

    public virtual bool AddUnit(Unit inUnit)
    {
        if (FindToken(inUnit) != null) { return false; }

        GameObject newtoken = Instantiate(unitToken, Vector3.zero, Quaternion.identity, transform);
        tokens.Add(newtoken);
        tableData.Add(inUnit);
        newtoken.GetComponent<UnitToken>().SetUnit(inUnit);
        ArrangeTokens();

        return true;
    }

    public bool RemoveUnit(Unit inUnit)
    {
        GameObject tToken = FindToken(inUnit);
        if (tToken == null) { return false; }

        tokens.Remove(tToken);
        Destroy(tToken);
        tableData.Remove(inUnit);
        ArrangeTokens();

        return true;
    }

    protected void ArrangeTokens()
    {
        int tokenCnt = tokens.Count;
        float tokenXlen = tokenSize;

        for (int i = 0; i < tokenCnt; i++)
        {
            float ind = i - tokenCnt / 2.0f + 0.5f;
            float xpos = ind * (tokenXlen + padding);
            tokens[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(xpos, 0, 0);
        }

        float xlen = (tokenCnt == 0) ? tokenXlen + padding * 2 : (tokenXlen + padding) * tokenCnt + padding;
        float ylen = gameObject.GetComponent<RectTransform>().sizeDelta.y;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(xlen, ylen, 0);
    }

    protected GameObject FindToken(Unit inUnit)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].GetComponent<PartyToken>().TokenOf(inUnit))
            {
                return tokens[i];
            }
        }

        return null;
    }
}
