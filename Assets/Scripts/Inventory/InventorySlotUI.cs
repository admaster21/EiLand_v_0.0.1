using UnityEngine;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI amountText;

    public void UpdateSlot(ItemStack itemStack)
    {
        if(itemStack == null)
        {
            itemNameText.text = "";
            amountText.text = "";
            return;
        }

        itemNameText.text = itemStack.itemName;
        amountText.text = itemStack.amount.ToString();
    }
}
