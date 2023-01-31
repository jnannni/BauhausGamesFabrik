using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    public PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        
        // to add the check that it is possible already open the inventory (that the player alrady talked to the golem)
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}
