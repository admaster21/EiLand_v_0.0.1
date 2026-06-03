using UnityEngine;

// extends Interactable, which means it can be interacted with by the player
public class ResourceNode : Interactable
{
    // name of the resource
    public string resourceName = "Stone";

    // amount of resouce that was harvested
    public int amount = 1;

    // whether the resource node should be destroyed after harvesting
    public bool destroyOnHarvest = true; 

    public override void Interact()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>(); // find the player's inventory

        if(playerInventory != null)
        {
            playerInventory.addItem(resourceName, amount); // add the harvested resource to the player's inventory
        }

        Debug.Log("Collected " + amount + " " + resourceName); // log the collected resource

        if(destroyOnHarvest)
        {
            Destroy(gameObject); // destroy the resource node if destroyOnHarvest is true
        }
    }
}
