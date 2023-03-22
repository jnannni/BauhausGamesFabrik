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
        if (thisItem)
        {
            itemName.text = thisItem.itemName;
        }
        //add names
        if (thisItem && SceneManager.GetActiveScene().name == "SampleScene")
        {
            currentImage = thisItem.itemImage;
        } else if (thisItem && SceneManager.GetActiveScene().name == "DreamWorld")
        {
            currentImage = thisItem.itemImageDW;
        }
    }

    public void ClickedOn()
    {        
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem, currentImage);
        }
    }

    private void Start()
    {
        selectionUISign = transform.Find("Selection").gameObject;
        selectionUISign.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " selected");
        selectionUISign.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " deselected");
        selectionUISign.SetActive(false);
    }
}
