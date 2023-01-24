using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;    
    [SerializeField] private Image itemImage;
    private EventSystem eventSystem;
    public InventoryItem currentItem;    

    public void SetTextAndButton(string description, bool isActive)
    {
        descriptionText.text = description;        
        if (isActive)
        {
            itemImage.gameObject.SetActive(true);            
        }
        else
        {
            itemImage.gameObject.SetActive(false) ;            
        }
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {                
                GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);                
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.myInventory[i], this);                    
                }                
            }
        }
    }
    
    void OnEnable()
    {        
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);        
    }

    private void Update()
    {        
        
    }

    void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void SetupDescriptionAndButton(string newDescription, bool isUsable, InventoryItem newItem, Sprite newItemImage)
    {
        currentItem = newItem;
        descriptionText.text = newDescription;        
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = newItemImage;
    }

    public void useButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(currentItem + "selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(currentItem + "deselected");
    }
}
