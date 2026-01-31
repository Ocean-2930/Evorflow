using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon_Base : CustomMouseInterface
{
    public ItemInst itemInstance;
    
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private GameObject itemAmount;
    [SerializeField] private GameObject itemPopup;

    public void SetItem(ItemInst initem)
    {
        itemInstance = initem;
        itemIcon.GetComponent<Image>().sprite = itemInstance.item.icon;
        itemIcon.GetComponent<Image>().SetNativeSize();
        SetAmount();
    }

    public void SetAmount()
    {
        if (itemInstance.item.stackType == StackType.None)
        {
            itemAmount.SetActive(false);
        }
        else
        {
            itemAmount.SetActive(true);
            itemAmount.GetComponent<TextMeshProUGUI>().text = itemInstance.amount.ToString();
        }
    }

    public void ResetSlot()
    {
        itemIcon.GetComponent<Image>().sprite = null;
        itemIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        itemAmount.SetActive(false);
        itemInstance = null;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        MousePopup.inst.CleanPopup();
    }
}
