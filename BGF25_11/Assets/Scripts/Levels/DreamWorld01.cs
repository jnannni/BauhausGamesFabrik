using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DreamWorld01  : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator golemAnimator;
    [SerializeField] private AnimationClip golemAnimationClip;
    [SerializeField] private BoolValue isInventoryAvailable;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject soundCutscene;
    public GameObject inventoryContent;
    public BoolValue isInventoryOpen;
    public BoolValue isPaused;
    public GameObject appearSpotPosition;
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    private FadeLayer fadeLayer;
    private bool cutSceneEnded = false;
    private bool openInventory;
    private bool trigger_waking02;
    private bool standup;
    private bool animationgolem;
    private bool laying;
    private bool stoneSelected;
    private bool hasClueA;
    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;
    private int iteration = 0;
    private GameObject inventorySelectedButton;
    private AudioManager audioManager;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02;
    private FMOD.Studio.EventInstance instance03;

    private void Awake()
    {        
        //player.transform.position = appearSpotPosition.transform.position;
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();        
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        dialogueRunner.LoadStateFromPlayerPrefs();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioManager.InitializeMusic(FMODEvents.instance.musicDW1);
        dialogueRunner.StartDialogue("TheDreamworld_01");
        inventorySelectedButton = inventoryContent.transform.GetChild(0).gameObject;
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_DreamWorld_001_TheGolemDream_Loopable");
        instance.start();
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
    }

    // Update is called once per frame
    void Update()
    {        
        variableStorage.TryGetValue("$openInventory", out openInventory);
        variableStorage.TryGetValue("$trigger_waking02", out trigger_waking02);
        variableStorage.TryGetValue("$standup", out standup);
        variableStorage.TryGetValue("$laying", out laying);
        variableStorage.TryGetValue("$stoneSelected", out stoneSelected);
        variableStorage.TryGetValue("$hasClueA", out hasClueA);
        variableStorage.TryGetValue("$animationgolem", out animationgolem);

        Debug.Log("hasClueA " + hasClueA);

        isInventoryAvailable.initialValue = openInventory;

        if (openInventory && iteration == 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(inventorySelectedButton);
            inventorySelectedButton.GetComponent<Button>().Select();
            isInventoryOpen.initialValue = true;
            inventoryPanel.SetActive(true);
            isPaused.initialValue = true;
            iteration++;
        }

        if (stoneSelected)
        {
            inventoryPanel.SetActive(false);
            Input.GetKeyDown("i");
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
        }

        if (trigger_waking02)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicDW1);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToWW, this.transform.position);*/
            dialogueRunner.SaveStateToPlayerPrefs();
            //isPaused.initialValue = true;

            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instanceallowed = false;
            }

            if (!cutSceneEnded)
            {
                if (instanceallowed02)
                {
                    instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CutScene_01_Golem");
                    instance02.start();
                    instanceallowed02 = false;
                }
                //isPaused.initialValue = false;
                canvasAnimator.SetBool("startCutSceneGolem", true);       
                //Destroy(soundCutscene);        
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);
            }            
            if (canvasAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("golemDialogue"))
            {
                canvasAnimator.SetBool("startCutSceneGolem", false);
                cutSceneEnded = true;
                transitionAnimator.SetBool("transitiontoww", true);  
                if (instanceallowed03)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance03 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_DW_to_WW");
                    instance03.start();
                    instanceallowed03 = false;
                }                              
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                //instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                //instance02.release();
                StartCoroutine(fadeLayer.FadeIn());
                instance03.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance03.release();
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);
                trigger_waking02 = false;
            }
        }

        if (laying)
        {
            playerAnimator.SetBool("laying", true);
        }

        if (standup)
        {
            playerAnimator.SetBool("laying", false);
            playerAnimator.SetBool("standup", true);
        }

        if (!standup)
        {
            playerAnimator.SetBool("standup", false);
        }

        if(animationgolem)
        {
            golemAnimator.SetBool("hearton", true);
        }
    }   
}