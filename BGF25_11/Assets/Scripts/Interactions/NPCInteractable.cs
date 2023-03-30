using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Yarn.Unity;
using FMODUnity;

public class NPCInteractable : MonoBehaviour
{
    public enum NPCState
    {
        idle,
        talk
    }

    private DialogueRunner dialogueRunner;    
    private bool isCurrentConversation = false;
    [SerializeField] private string dialogueStartingNode;    
    [SerializeField] private Animator animator;
    public Animator arrowAnimator;
    public BoolValue isDialogueRunning;
    public Material outlineShader;
    private Shader defaultShader;    
    private StudioEventEmitter emitter;
    private bool isInTheRange;

    public bool interactable;    
    public NPCState npcState;    
    public int npcIndex = 0;
    public string npcName;
    public GameObject target;
    private bool isPressed;
    private float distance;
    public float minDist = 5f;    

    private void Start()
    {
        defaultShader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");        
        this.transform.gameObject.GetComponent<SpriteRenderer>().material.shader = defaultShader;        
        if (animator == null)
        {
            Debug.Log("No Animator Needed");
        }
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();        
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        isPressed = false;
        
        //emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.npsAura[1], this.gameObject);
        //emitter.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInTheRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTheRange = false;
    }

    void Update()
    {        
        isPressed = Input.GetKeyDown(KeyCode.K);
        distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position);
        if (isInTheRange && interactable && !isDialogueRunning.initialValue)
        {
            this.transform.gameObject.GetComponent<SpriteRenderer>().material.shader = outlineShader.shader;            
        }
        else
        {
            this.transform.gameObject.GetComponent<SpriteRenderer>().material.shader = defaultShader;            
        }
        Interact();        
        if (!isDialogueRunning.initialValue)
        {            
            ChangeDirection(0, -1);            
        }
    }   

    public void Interact()
    {        
        if (isInTheRange && isPressed && interactable)
        {            
            if (animator)
            {
                ChangeDirection(-target.GetComponent<Animator>().GetFloat("Horizontal"),
                            -target.GetComponent<Animator>().GetFloat("Vertical"));
            }            
            StartConversation();            
        } else if (isInTheRange && !interactable) {
            if (animator)
            {
                ChangeDirection(-target.GetComponent<Animator>().GetFloat("Horizontal"),
                                -target.GetComponent<Animator>().GetFloat("Vertical"));
            }
            if (!isDialogueRunning.initialValue)
            {
                StartConversation();
            }                        
        }     
    }

    void ChangeDirection(float x, float y)
    {
        if (animator.GetFloat("Horizontal") != 0 && animator.GetFloat("Vertical") != 0)
        {
            animator.SetFloat("Horizontal", x);
            animator.SetFloat("Vertical", y);
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
