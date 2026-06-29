using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventorySlotUI[] slotUis;
   
    void Start()
    {
        RefreshInventoryUI();
    }

    // Updates each UI slot to match the item data stored in the player's inventory.
    public void RefreshInventoryUI()
    {
        for (int i = 0; i < slotUis.Length; i++)
        {
            if (i < playerInventory.slots.Length)
            {
                slotUis[i].UpdateSlot(playerInventory.slots[i]);
            }
        }
    }
}
