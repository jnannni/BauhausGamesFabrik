using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("UI Stuff to change")]        
    [SerializeField] private TextMeshProUGUI itemName;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    private Sprite currentImage;
    private GameObject selectionUISign;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
       
        if (thisItem && (SceneManager.GetActiveScene().name == "DreamWorld1" ||
            SceneManager.GetActiveScene().name == "DreamBlocks" ||
            SceneManager.GetActiveScene().name == "Gravedream" ||
            SceneManager.GetActiveScene().name == "FrozenVillage" ||
            SceneManager.GetActiveScene().name == "RedBeach"))
        {            
            currentImage = thisItem.itemImageDW;
            itemName.text = thisItem.itemNameDW;
        } else
        {
            currentImage = thisItem.itemImage;
            itemName.text = thisItem.itemName;
        }
    }

    public void ClickedOn()
    {        
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem, currentImage);
            thisItem.Use();
        }
    }

    private void Start()
    {
        selectionUISign = transform.Find("Selection").gameObject;
        selectionUISign.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {        
        selectionUISign.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {        
        selectionUISign.SetActive(false);
    }
}
