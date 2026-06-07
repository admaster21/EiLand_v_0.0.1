using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public float interactionRange = 3f; // Range within which the player can interact with objects
    public PlayerInventory playerInventory;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerInventory.PrintInventory();
        }
    }

    void TryInteract()
    {
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward,
             out RaycastHit hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
