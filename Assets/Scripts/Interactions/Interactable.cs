using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        // This method can be overridden by subclasses to define specific interaction behavior.
        Debug.Log("Collected " + gameObject.name);
    }
}
