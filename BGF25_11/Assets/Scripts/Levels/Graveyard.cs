using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Graveyard : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    public Animator scaredCatAnimator;
    public Animator transitionAnimator;

    private bool putintheinventory002;
    private bool enterthechurch;
    private bool trigger_TheChurch;
    private bool scurryawayanimation;
    private bool exitthechurch;
    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;

    private AudioManager audioManager;
    
    private bool isInsideTheChurch;
    private FadeLayer fadeLayer;    
    [SerializeField] private InventoryItem item002;
    [SerializeField] private GameObject item002Object;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject catCreature;
    [SerializeField] private GameObject insideTheChurch;
    [SerializeField] private GameObject outsideTheChurch;
    [SerializeField] private PlayerInventory playerInventory;  
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02;  
    private FMOD.Studio.EventInstance instance03;  
    private float distanceToCat;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        dialogueRunner.LoadStateFromPlayerPrefs();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_003_1_TheGraveyard_Loopable");
        instance.start();
        isInsideTheChurch = false;
        dialogueRunner.StartDialogue("TheGraveYard");
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToCat = Vector2.Distance(player.transform.position, catCreature.transform.position);
        variableStorage.TryGetValue("$trigger_TheChurch", out trigger_TheChurch);
        variableStorage.TryGetValue("$scurryawayanimation", out scurryawayanimation);                
        variableStorage.TryGetValue("$putintheinventory002", out putintheinventory002);
        variableStorage.TryGetValue("$enterthechurch", out enterthechurch);
        variableStorage.TryGetValue("$exitthechurch", out exitthechurch);

        if (scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", true);
        } else if (!scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", false);
        }

        if (trigger_TheChurch)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicGraveyard);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToDW, this.transform.position);*/
            dialogueRunner.SaveStateToPlayerPrefs();
            player.transform.localScale = new Vector3(1f, 1f, player.transform.localScale.z);
            transitionAnimator.SetBool("transitiontodw", true);
            if (instanceallowed03)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance03 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_WW_to_DW");
                    instance03.start();
                    instanceallowed03 = false;
                }  
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontodw"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontodw", false);
                instance03.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);;
                instance03.release();
            }                        
        }

        if (enterthechurch && !isInsideTheChurch)
        {
            //audioManager.InitializeMusic(FMODEvents.instance.musicChurch);
            // move the character to the church position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(insideTheChurch.transform.position.x, insideTheChurch.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInsideTheChurch = true;
            variableStorage.SetValue("$enterthechurch", false);
            player.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_003_2_TheGraveyard_InsideChurch_Loopable");
                instance02.start();
                instanceallowed = false;
                instanceallowed02 = true;
            }
        }

        if (exitthechurch && isInsideTheChurch)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(outsideTheChurch.transform.position.x, outsideTheChurch.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInsideTheChurch = false;
            variableStorage.SetValue("$exitthechurch", false);
            player.transform.localScale = new Vector3(1f, 1f, 0f);
            if (instanceallowed02)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_003_1_TheGraveyard_Loopable");
                    instance.start();
                    instanceallowed02 = false;
                    instanceallowed = true;
                }
        }

        if (putintheinventory002)
        {
            if (!playerInventory.myInventory.Contains(item002))
            {
                playerInventory.myInventory.Add(item002);
                item002Object.GetComponent<ObjectInteractable>().enabled = false;
            }
        }        

        if (distanceToCat > 5)
        {
            variableStorage.SetValue("$scurryawayanimation", false);
        }
    }
}
