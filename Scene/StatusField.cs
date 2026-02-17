using TMPro;
using UnityEngine;

public class StatusField : MonoBehaviour
{
    [SerializeField] private GameObject HP;
    [SerializeField] private GameObject HPBar;
    [SerializeField] private GameObject CON;
    [SerializeField] private GameObject CONBar;
    [SerializeField] private GameObject STR;
    [SerializeField] private GameObject INT;
    [SerializeField] private GameObject AGI;
    [SerializeField] private GameObject VIT;

    public void UpdateStatus(UnitInst unit)
    {
        if (HP != null)
        {
            HP.GetComponent<TextMeshProUGUI>().text = $"{unit.stat[StatType.HP]}/{unit.stat[StatType.MAXHP]}";
        }

        if (HPBar != null)
        {
            float ratio = (float)unit.stat[StatType.HP] / unit.stat[StatType.MAXHP];
            if (1 < ratio)
            {
                ratio = 1.0f;
            }
            HPBar.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);
        }

        if (CON != null)
        {
            CON.GetComponent<TextMeshProUGUI>().text = $"{unit.stat[StatType.CON]}/{unit.stat[StatType.MAXCON]}";
        }

        if (CONBar != null)
        {
            if (unit.stat[StatType.MAXCON] == 0)
            {
                CONBar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            }
            else
            {
                float ratio = (float)unit.stat[StatType.CON] / unit.stat[StatType.MAXCON];
                if (1 < ratio)
                {
                    ratio = 1.0f;
                }
                CONBar.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);
            }
        }

        if (STR != null)
        {
            STR.GetComponent<TextMeshProUGUI>().text = unit.stat[StatType.STR].ToString();
        }

        if (INT != null)
        {
            INT.GetComponent<TextMeshProUGUI>().text = unit.stat[StatType.INT].ToString();
        }

        if (AGI != null)
        {
            AGI.GetComponent<TextMeshProUGUI>().text = unit.stat[StatType.AGI].ToString();
        }

        if (VIT != null)
        {
            VIT.GetComponent<TextMeshProUGUI>().text = unit.stat[StatType.VIT].ToString();
        }
    }

    public void UpdateStatus(Table t)
    {
        if (STR != null)
        {
            STR.GetComponent<TextMeshProUGUI>().text = t.stren.ToString();
        }

        if (INT != null)
        {
            INT.GetComponent<TextMeshProUGUI>().text = t.intel.ToString();
        }

        if (AGI != null)
        {
            AGI.GetComponent<TextMeshProUGUI>().text = t.agil.ToString();
        }
    }
}
