using UnityEngine;
using System.Collections;

// extends Interactable, which means it can be interacted with by the player
public class ResourceNode : Interactable
{
    
    // name of the resources
    public string resourceName = "Stone";
    

    // amount of resouce that was harvested
    public int amount = 1;

    // whether the resource node should be destroyed after harvesting
    public bool destroyOnHarvest = true;
    
    // variables to set a boolean to respawn and a timer.
    public bool canRespawn = true;
    public float respawnTime = 10f;


    public override void Interact()
    {
        PlayerInventory playerInventory = FindFirstObjectByType<PlayerInventory>(); // find the player's inventory

        if(playerInventory != null)
        {
            playerInventory.addItem(resourceName, amount); // add the harvested resource to the player's inventory
        }

        if(destroyOnHarvest)
        {
            if(canRespawn)
            {
                StartCoroutine(RespawnRoutine());
            }
            else
            {
                Destroy(gameObject); // destroy the resource node if destroyOnHarvest is true
            }
        }

    }

    IEnumerator RespawnRoutine()
    {
        SetNodeActive(false); // turns Node off

        yield return new WaitForSeconds(respawnTime); //sets timer for respwan

        SetNodeActive(true); // turns Node back on
    }

    void SetNodeActive(bool isActive)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = isActive;
        }

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isActive;
        }
    }
}
