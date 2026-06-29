
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public ItemStack[] slots = new ItemStack[24];
    public InventoryDisplay inventoryDisplay;

    public void addItem(string itemName, int quantity)
    {
        //searches the inventory for a pre-exsiting Item in the inventory
        //if found, it adds to the pre-exsisting stack
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].itemName == itemName)
            {
                slots[i].amount += quantity;
                Debug.Log(itemName + " +" + quantity + " added to Inventory");
                inventoryDisplay.RefreshInventoryUI();
                return;
            }
        }
        //searches for the item in the inventory
        //adds item to first avaiable slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i]  == null || string.IsNullOrEmpty(slots[i].itemName))
            {
                slots[i] = new ItemStack();
                slots[i].itemName = itemName;
                slots[i].amount = quantity;
                Debug.Log(itemName + " +" + quantity + " added to Inventory");
                inventoryDisplay.RefreshInventoryUI();
                return;
            }
        }
        //print for debug console that inventory is full
        Debug.Log("Inventory is Full!");
    }

    // Method to get the quantity of a specific item in the inventory
    public int GetItemQuantity(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].itemName == itemName)
            {
                return slots[i].amount;
            }
        }
        //return 0 if item isnt found
        return 0; 
    }

   
}
