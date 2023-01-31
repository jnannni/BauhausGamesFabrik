using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public Sprite itemImageDW;
    public bool usable;
    public bool isUsed = false;
    public bool collectable = true;
    public UnityEvent thisEvent;
    [SerializeField] private GameObject useButton;

    public void Use()
    {        
        thisEvent.Invoke();       
        isUsed = !isUsed;        
    }    
}
