using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class PhysicalInvetoryItem : MonoBehaviour
{    
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem item;
    private bool isPressed;
    private bool isItemClose;
    public Animator animator;
    [SerializeField] private string pickedUpAnim = "PickedUp";

    private InMemoryVariableStorage variableStorage;
    private bool takethekey = false;

    private void Awake()
    {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
    }
    private void Start()
    {
        isPressed = false;
        isItemClose = false;
        
    }

    private void Update()
    {        
        isPressed = Input.GetKeyDown(KeyCode.K);
        if (isPressed && isItemClose && item.collectable)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.collectedItem, this.transform.position);
            AddItemToInventory();
            animator.Play("PickedUp", 0, 0f);           
            Destroy(this.gameObject);            
        }
        /*if (isItemClose && !item.collectable)
        {            
            AddItemToInventory();            
            Destroy(this.gameObject);
        }*/
        /*variableStorage.TryGetValue("$takethekey", out takethekey);
        if (takethekey)
        {
            SceneManager.LoadScene("DreamWorld1");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isItemClose = true;
            
        }
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isItemClose = false;
        }
    }

    public void AddItemToInventory()
    {
        if (playerInventory && item)
        {
            playerInventory.myInventory.Add(item);
        }
    }

    public void AddingItemFromDialogue(InventoryItem itemFromDialogue, PlayerInventory curInventory)
    {
        Debug.Log("to inv");
        AudioManager.instance.PlayOneShot(FMODEvents.instance.collectedItem, this.transform.position);
        if (curInventory && itemFromDialogue)
        {
            curInventory.myInventory.Add(itemFromDialogue);
        }
        animator.Play("PickedUp", 0, 0f);
    }
}
