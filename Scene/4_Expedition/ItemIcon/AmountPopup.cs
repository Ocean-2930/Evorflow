using TMPro;
using UnityEngine;

public class AmountPopup : MonoBehaviour
{
    [SerializeField] private GameObject amountField;

    public void UpdateAmount(int amount)
    {
        amountField.GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }
}
