using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OnGroundInteractable : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation = false;   

    [SerializeField] private InventoryItem item;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private string dialogueStartingNode;
    [SerializeField] GameObject iceClue;
    public BoolValue isDialogueRunning;

    public GameObject target;    
    private float distance;
    public float minDist = 0.5f;    
    private bool isPressed;
    private bool isInTheRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInTheRange = true;
        iceClue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTheRange = false;
        iceClue.SetActive(false);
    }

    private void Start()
    {
        
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);        
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        isPressed = Input.GetKeyDown(KeyCode.K);
        distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position);;
        Interact();
    }

    public void Interact()
    {
        if (isInTheRange && isPressed && dialogueStartingNode != "" && !isDialogueRunning.initialValue)
        {            
            StartConversation();
        }
    }

    private void StartConversation()
    {
        Debug.Log($"Started conversation with {name}.");        
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(dialogueStartingNode);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {            
            isCurrentConversation = false;
            Debug.Log($"Started conversation with {name}.");
        }
    }
}
