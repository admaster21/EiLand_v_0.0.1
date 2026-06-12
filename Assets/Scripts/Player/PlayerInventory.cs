using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Inventory dictionary to store item names and their quantities
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public void addItem(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity; // Increase quantity if item already exists
        }
        else
        {
            inventory[itemName] = quantity; // Add new item to inventory
        }

        Debug.Log(itemName + " added to inventory. " + quantity);
    }

    // Method to get the quantity of a specific item in the inventory
    public int GetItemQuantity(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            return inventory[itemName]; // Return quantity of the specified item
        }
        else
        {
            Debug.LogWarning("Item not found in inventory: " + itemName);
            return 0; // Return 0 if item is not found
        }
    }

    public void PrintInventory()
    {
        Debug.Log(GetInventoryDisplay());
    }

    private string GetInventoryDisplay()
    {
        if(inventory.Count == 0)
        {
            return "Inventory is Empty!";
        }

        string inventoryDisplay = "Inventory:\n";

        foreach (KeyValuePair<string, int> item in inventory)
        {
            inventoryDisplay += "- " + item.Key + ": " + item.Value + "\n";
        }

        return inventoryDisplay;
    }
}
