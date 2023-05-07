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
    public AutomaticInteractions02 anotherscript;
    public CameraMovement anotherscript02;
    public Rigidbody2D playerRb;

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
        audioManager.InitializeMusic(FMODEvents.instance.musicBeach);
        dialogueRunner.StartDialogue("TheDreamworld_05");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_waking05", out trigger_waking05);
        variableStorage.TryGetValue("$snakeanimation", out snakeanimation);

        if(anotherscript.enteredbox && !anotherscript02.complicated)
        {
            //playerRb = GetComponent<Rigidbody2D>();            
            playerRb.bodyType = RigidbodyType2D.Static;
            playerAnimator.enabled = false;
        }

        else
        {
            playerRb.bodyType = RigidbodyType2D.Dynamic;
            playerAnimator.enabled = true;
        }

        if(trigger_waking05)
        {
            transitionAnimator.SetBool("transitiontoww", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);                
            }
        }

        if(snakeanimation)
        {
            SnakesAnimator.SetBool("snakesmove", true);
        }
    }
}
