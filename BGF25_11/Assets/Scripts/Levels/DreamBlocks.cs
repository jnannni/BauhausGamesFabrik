using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;


public class DreamBlocks : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private FadeLayer fadeLayer;
    private BoxCollider2D[] boxCollider2Ds;

    private bool trigger_waking04;
    private bool approaching_parents;
    private bool swimout;
    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parents;
    [SerializeField] private GameObject closeToParents;
    [SerializeField] private GameObject secondPartSpot;
    [SerializeField] private BoolValue isMaskUsed;
    [SerializeField] private float approachSpeed = 2f;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private BoolValue isInventoryOpen;
    [SerializeField] private BoolValue isPaused;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private Animator crossfadeAnimator;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02; 
    private FMOD.Studio.EventInstance instance03; 
    
    

    private AudioManager audioManager;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_004_TheBlocks_Loopable");
        instance.start();
        boxCollider2Ds = parents.GetComponents<BoxCollider2D>();        
        dialogueRunner.StartDialogue("TheDreamworld_03");
        dialogueRunner.LoadStateFromPlayerPrefs();
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$approaching_parents", out approaching_parents);
        variableStorage.TryGetValue("$trigger_waking04", out trigger_waking04);
        variableStorage.TryGetValue("$swimout", out swimout);

        if (trigger_waking04)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicDreamBlocks);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToWW, this.transform.position);*/
            dialogueRunner.SaveStateToPlayerPrefs();
            transitionAnimator.SetBool("transitiontoww", true);
            if (instanceallowed03)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instance03 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_DW_to_WW");
                instance03.start();
                instanceallowed03 = false;
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);;
                instance.release();
            }
        }

        if (isMaskUsed.initialValue)
        {
            //StartCoroutine(fadeLayer.FadeIn());
            crossfadeAnimator.SetBool("Start1", true);
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            isMaskUsed.initialValue = false;
            canvasAnimator.SetBool("divingon", true);
            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CutScene_01_Golem");
                instance02.start();
                instanceallowed = false;
            }
            dialogueRunner.StartDialogue("TheDive_After");
            player.transform.position = new Vector3(secondPartSpot.transform.position.x, secondPartSpot.transform.position.y, 0f);
            //StartCoroutine(fadeLayer.FadeOut());

        }

        if (swimout)
        {
            canvasAnimator.SetBool("divingon", false);
            crossfadeAnimator.SetBool("Start1", false);
            if (instanceallowed02)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_004_TheBlocks_Loopable");
                    instance.start();
                    instanceallowed02 = false;
                }
        }

        if (approaching_parents && player.transform.position != closeToParents.transform.position)
        {
            playerAnimator.SetFloat("Horizontal", -1f);
            playerAnimator.SetFloat("Vertical", 0);
            playerAnimator.SetBool("walking02", true);
            player.transform.position = Vector3.MoveTowards(player.transform.position, closeToParents.transform.position, approachSpeed * Time.deltaTime);
            boxCollider2Ds[0].enabled = false;
            boxCollider2Ds[1].enabled = true;
            ///variableStorage.SetValue("$approaching_parents", false);
        }

        if(player.transform.position == closeToParents.transform.position)
        {
            approaching_parents = false;  
            playerAnimator.SetBool("walking02", false);          
        }
    }
}
