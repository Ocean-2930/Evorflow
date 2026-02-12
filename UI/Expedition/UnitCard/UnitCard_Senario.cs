using UnityEngine;
using UnityEngine.UI;

public class UnitCard_Scenario : UnitCard
{
    [SerializeField] private GameObject checkMark;
    private static BenchTable benchTable;

    public void ToggleCard()
    {
        checkMark.SetActive(!checkMark.activeInHierarchy);
    }

    public void OpenCard()
    {
        if (!cardside)
        {
            FlipCard();
        }
    }

    public void FlipCard()
    {
        Color temp = cardIllust.GetComponent<Image>().color;
        temp.a = cardside ? 0.4f : 1.0f;
        cardIllust.GetComponent<Image>().color = temp;
        itemSlot.SetActive(cardside);
        cardside = !cardside;
    }

    public void OnLeftClick()
    {
        if (!cardside)
        {
            return;
        }

        if(benchTable == null)
        {
            Transform buff = transform;
            while (transform.parent != null)
            {
                BenchTable t = buff.GetComponent<BenchTable>();
                if (t != null)
                {
                    benchTable = t;
                    break;
                }
                buff = buff.parent;
            }
        }
        
        benchTable.SwitchParty(unit);
    }

    public void OnRightClick()
    {
        if (!checkMark.activeInHierarchy)
        {
            FlipCard();
        }
    }
}
