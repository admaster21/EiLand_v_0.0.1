using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Transform interactionPoint; // The point where the players interactions orgininate from.
    public TextMeshProUGUI collectPrompt; //Varible for the crosshair collect detection.

    public float baseHitDamage = 20f;
    public float hitInterval = 0.5f;
    public float nextHitTime;

    public float interactionRange = 3f; // Range within which the player can interact with objects
    public PlayerInventory playerInventory;
    
    void Start()
    {
        collectPrompt.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && Time.time >= nextHitTime)
        {
            TryInteract();
            nextHitTime = Time.time + hitInterval;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerInventory.PrintInventory();
        }
        UpdateCollectPrompt();//updates crosshair promt message.
    }

    void TryInteract()
    {
        if (!Physics.Raycast(
            interactionPoint.position,
            interactionPoint.forward,
            out RaycastHit hit,
            interactionRange))
        {
            return;
        }

        Interactable interactable =
            hit.collider.GetComponentInParent<Interactable>();

        if (interactable is ResourceNode resourceNode)
        {
            resourceNode.TakeDamage(baseHitDamage);
        }
        else if (interactable != null)
        {
            interactable.Interact();
        }
    }

    //if hovering over rescource message "press E to Collect" will be prompted
    void UpdateCollectPrompt()
    {
        if(Physics.Raycast(interactionPoint.position, interactionPoint.forward, out RaycastHit hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

            if(interactable != null)
            {
                collectPrompt.gameObject.SetActive(true);
                return;
            }
        }

        collectPrompt.gameObject.SetActive(false);
    }
}
