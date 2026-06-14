using UnityEngine;
using System.Collections;

// extends Interactable, which means it can be interacted with by the player
public class ResourceNode : Interactable
{
    public GameObject floatingDamagePrefab;
    public float MAX_Health = 150;// the maxium health for destoryable resourses
    private float currentHealth;// the current health of object
    public string resourceName = "";// name of the resources (with empty string)
    public int amount = 1;// amount of resouce that was harvested
    public bool destroyOnHarvest = true;// whether the resource node should be destroyed after harvesting
    
    // variables to set a boolean to respawn and a timer.
    public bool canRespawn = true;
    public float respawnTime = 10f;

    void Start()
    {
        currentHealth = MAX_Health;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameObject damagePopup = Instantiate(
        floatingDamagePrefab,
        transform.position + Vector3.up * 2f,
        Quaternion.identity
        );

        damagePopup.GetComponent<FloatingDamageText>().SetDamage(damage);

        if (currentHealth <= 0f)
        {
            Interact();
        }
    }


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

        currentHealth = MAX_Health;
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
