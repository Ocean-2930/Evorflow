using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemIcon_Bag : ItemIcon_Base
{
    [SerializeField] private GameObject grabObject;
    [SerializeField] private GameObject amountPopup;

    private GameObject popupInstance;
    private int grabAmount = 1;

    public override void OnLeftClick()
    {
        if(itemInstance == null)
        {
            return;
        }

        int amt;
        if (itemInstance.item.stackType == StackType.stack)
        {
            amt = grabAmount;
        }
        else
        {
            amt = itemInstance.amount;
        }
        grabAmount = 1;

        GameObject obj = Instantiate(grabObject, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(gameObject.GetComponentInParent<Canvas>().gameObject.transform);
        ItemInst grabItem = new ItemInst();
        grabItem.item = itemInstance.item;
        grabItem.amount = amt;
        obj.GetComponent<ItemIcon_Grab>().SetItem(grabItem);
        obj.GetComponent<ItemIcon_Grab>().bagInst = itemInstance;
        obj.GetComponent<CanvasGroup>().alpha = 0.4f;
        MouseControl.inst.Grab(obj.GetComponent<ItemIcon_Grab>());
    }

    public override void OnRightClick()
    {
        if (itemInstance == null)
        {
            return;
        }

        if (itemInstance.item.stackType != StackType.stack)
        {
            return;
        }

        if (popupInstance != null)
        {
            return;
        }

        MousePopup.inst.CleanPopup();
        popupInstance = Instantiate(amountPopup, Vector3.zero, Quaternion.identity);
        grabAmount = 1;
        popupInstance.GetComponent<AmountPopup>().UpdateAmount(grabAmount);
        MousePopup.inst.ViewPopup(popupInstance);
    }

    public override void OnHover()
    {
        if(popupInstance == null)
        {
            return;
        }

        float scroll = Mouse.current.scroll.ReadValue().y;
        if (0 < scroll && grabAmount < itemInstance.amount)
        {
            grabAmount++;
            popupInstance.GetComponent<AmountPopup>().UpdateAmount(grabAmount);
        }
        else if (scroll < 0 && 1 < grabAmount)
        {
            grabAmount--;
            popupInstance.GetComponent<AmountPopup>().UpdateAmount(grabAmount);
        }
    }
}
