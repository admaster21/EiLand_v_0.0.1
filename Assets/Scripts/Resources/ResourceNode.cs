using UnityEngine;

// extends Interactable, which means it can be interacted with by the player
public class ResourceNode : Interactable
{
    public string resourceName = "Stone"; // name of the resource
    public int amount = 1; // amount of resouce that was harvested
    public bool destoryOnHarvest = true; // whether the resource node should be destroyed after harvesting

    public override void Interact()
    {
        Debug.Log("Collected " + amount + " " + resourceName); // log the collected resource

        if(destoryOnHarvest)
        {
            Destroy(gameObject); // destroy the resource node if destoryOnHarvest is true
        }
    }
}
