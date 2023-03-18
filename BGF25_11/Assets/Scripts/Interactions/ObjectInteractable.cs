using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ObjectInteractable : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation = false;   
    [SerializeField] private InventoryItem item;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private string dialogueStartingNode;
    public Animator animator;
    public BoolValue isDialogueRunning;
    public Material outlineShader;
    private Shader defaultShader;

    public bool collectable;
    public GameObject target;    
    private float distance;
    public float minDist = 0.5f;    
    private bool isPressed;
    private bool isInTheRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInTheRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTheRange = false;
    }

    private void Start()
    {
        defaultShader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");
        this.transform.gameObject.GetComponent<SpriteRenderer>().material.shader = defaultShader;
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);        
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        isPressed = Input.GetKeyDown(KeyCode.K);
        distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position);
        if (isInTheRange && !isDialogueRunning.initialValue)
        {
            this.transform.gameObject.GetComponent<SpriteRenderer>().material.shader = outlineShader.shader;            
        }
        else this.transform.gameObject.GetComponent<SpriteRenderer>().material.shader = defaultShader;
        Interact();
    }

    public void Interact()
    {
        if (isInTheRange && isPressed)
        {
            StartConversation();
            if (item && playerInventory && !collectable)
            {
                playerInventory.myInventory.Add(item);
                Destroy(this.gameObject);
            }
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
