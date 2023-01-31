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
    //private StudioEventEmitter emitter;

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

    void Update()
    {        
        isPressed = Input.GetKeyDown(KeyCode.K);
        distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position);
        if (distance < minDist && interactable)
        {
            arrowAnimator.SetBool("showArrow", true);
        }
        else
        {
            //emitter.Stop();
            arrowAnimator.SetBool("showArrow", false);
        }
        Interact();
        if (!isDialogueRunning.initialValue)
        {            
            ChangeDirection(0, -1);
        }
    }   

    public void Interact()
    {        
        if (distance < minDist && isPressed && interactable)
        {
            if (animator)
            {
                ChangeDirection(-target.GetComponent<Animator>().GetFloat("Horizontal"),
                            -target.GetComponent<Animator>().GetFloat("Vertical"));
            }            
            StartConversation();
            Debug.Log("interactable npc");
        } else if (distance < minDist && !interactable) {
            if (animator)
            {
                ChangeDirection(-target.GetComponent<Animator>().GetFloat("Horizontal"),
                                -target.GetComponent<Animator>().GetFloat("Vertical"));
            }
                        
            StartConversation();            
        }     
    }

    void ChangeDirection(float x, float y)
    {
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", y);
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
