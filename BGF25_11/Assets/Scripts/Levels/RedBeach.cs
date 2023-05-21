using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RedBeach : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool trigger_waking05;
    private bool snakeanimation;
    private bool dialogue;
    private bool cupselected;

    private FadeLayer fadeLayer;
    private AudioManager audioManager;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private InventoryItem item12;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject dustParticleSystem;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator GlobalLight;
    [SerializeField] private Animator Fader;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private Animator SnakesAnimator;
    [SerializeField] private GameObject inventoryPanel;
    public BoolValue isPaused;
    public AutomaticInteractions02 anotherscript;
    public CharacterMovement characterScript;
    public CameraMovement anotherscript02;
    public Rigidbody2D playerRb;
    [SerializeField] public Transform cameraanchor;
    public SpriteRenderer celestial;
    public BoxCollider2D celestialBox;
    public Transform rabbitend;
    [SerializeField] public GameObject particles;
    [SerializeField] public GameObject trigger;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02;
    [SerializeField] public Transform positionCelestial;
    public BoolValue isInventoryOpen;
    private bool instanceallowed;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioManager.InitializeMusic(FMODEvents.instance.musicBeach);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_DreamWorld_005_TheRedBeach_Loopable");
        instance.start();
        dialogueRunner.StartDialogue("TheDreamworld_05");
        instanceallowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_waking05", out trigger_waking05);
        variableStorage.TryGetValue("$snakeanimation", out snakeanimation);
        variableStorage.TryGetValue("$cupselected", out cupselected);

        if(anotherscript.enteredbox && !anotherscript02.complicated)
        {
            //playerRb = GetComponent<Rigidbody2D>();            
            //playerRb.bodyType = RigidbodyType2D.Static;
            //playerAnimator.enabled = false;
            playerAnimator.SetBool("walking", false);
            playerAnimator.SetFloat("Speed", 0);
            characterScript.enabled=false;
        }

        if(cameraanchor.transform.position == positionCelestial.transform.position)
        {
            player.transform.position = new Vector3 (-49.38979f, -32.28f, 0f);
            playerAnimator.SetFloat("Horizontal", -1);
            playerAnimator.SetFloat("Vertical", 0);
            celestial.enabled = true;
            celestialBox.enabled = true;
            Destroy(particles);
            Destroy(trigger);
            positionCelestial.position = new Vector3 (0f, 0f, 0f);
            dialogueRunner.StartDialogue("TheRabbit_Celestial");    
        }

        if(trigger_waking05)
        {
            transitionAnimator.SetBool("transitiontoww", true);
            if (instanceallowed)
                {
                    instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance.release();
                    instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_DW_to_WW");
                    instance02.start();
                    instanceallowed = false;
                }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance02.release();
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);                
            }
        }

        if(snakeanimation)
        {
            SnakesAnimator.SetBool("snakesmove", true);
        }

        if(cameraanchor.transform.position == rabbitend.transform.position)
        {  
        }

        if(celestial.enabled == true)
        {
            characterScript.enabled = true;
            //rabbitend.transform.position = new Vector3(0,0,0);
        }

        if(cupselected)
        {
            inventoryPanel.SetActive(false);
            Input.GetKeyDown("i");
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
        }
    }
}
