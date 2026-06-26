using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}
