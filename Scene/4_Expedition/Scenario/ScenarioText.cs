using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.InputSystem.Editor.InputActionCodeGenerator;
using static UnityEngine.Rendering.DebugUI;

public enum Language
{
    Korean = 0,
    English = 1,
    None = 2
}

public class ScenarioText : MonoBehaviour
{
    public event Action PartyChanged;

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
    [SerializeField] private GameObject benchTable;
    private BenchTable _btable;
    private BenchTable btable
    {
        get
        {
            if (_btable != null)
            {
                return _btable;
            }
            _btable = benchTable.GetComponent<BenchTable>();
            return _btable;
        }
    }
    [SerializeField] private GameObject partyTable;
    private PartyTable _ptable;
    private PartyTable ptable
    {
        get
        {
            if (_ptable != null)
            {
                return _ptable;
            }
            _ptable = partyTable.GetComponent<PartyTable>();
            return _ptable;
        }
    }
    public bool tableOpened { get { return partyTable.activeInHierarchy; } }

    [SerializeField] private float optionSpacing = 140.0f;

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

    public Table partyTableData { get { return ptable.tableData; } }

    public void OpenOptions(string[] inoptions, Action<int> onOption)
    {
        string[] options;
        if (inoptions.Length == 0) { options = new string[1]; options[0] = "No Text"; }
        else { options = inoptions; }

        float ypos = 0.0f;
        for (int i = 0; i < options.Length; i++)
        {
            string optionText = options[i];
            GameObject obj = Instantiate(optionBox);
            obj.GetComponent<OptionBox>().Initialize(onOption, i, optionText);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.SetParent(optionTransform);
            rect.anchoredPosition = new Vector3(0, ypos, 0);
            ypos -= optionSpacing;
        }
    }

    public void CleanOptions()
    {
        for (int i = optionTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(optionTransform.GetChild(i).gameObject);
        }
    }

    public void OpenTable()
    {
        partyTable.SetActive(true);
    }

    public void AddParty(UnitInst unit)
    {
        if (!partyTable.activeInHierarchy)
        {
            return;
        }
        PartyChanged?.Invoke();
        ptable.AddUnit(unit);
    }

    public void RemoveParty(UnitInst unit)
    {
        if (!partyTable.activeInHierarchy)
        {
            return;
        }
        PartyChanged?.Invoke();
        ptable.RemoveUnit(unit);
    }

    public void CloseTable()
    {
        ptable.CleanTable();
        btable.CleanTable();
        partyTable.SetActive(false);
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
