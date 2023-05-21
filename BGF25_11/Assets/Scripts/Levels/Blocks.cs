using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Blocks : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;    

    private bool entertheantique;
    private bool trigger_TheAllKnowingLady;
    private bool putintheinventory003;
    private bool exittheantique;
    private bool poetsdiary;
    private bool coinisselected;
    private bool putintheinventory008;
    private bool putintheinventory001;
    private bool showthedrawing;

    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;

    private bool isInAntiqueShop;
    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject inFrontOfAntique;
    [SerializeField] private GameObject insideOfAntique;
    [SerializeField] private InventoryItem item003;
    [SerializeField] private GameObject item003Object;
    [SerializeField] private InventoryItem item008;
    [SerializeField] private InventoryItem coinForFountain;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private BoolValue isInventoryOpen;
    [SerializeField] private BoolValue isPaused;
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;
    [SerializeField] private Sprite poetsDairyImage;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02; 
    private FMOD.Studio.EventInstance instance03;

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
        isInAntiqueShop = false;
        dialogueRunner.StartDialogue("TheBlocks");
        variableStorage.SetValue("$putintheinventory001", true);
        dialogueRunner.LoadStateFromPlayerPrefs();
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_DreamWorld_003_TheBlocks_Loopable");
        instance.start();
        //audioManager.InitializeMusic(FMODEvents.instance.musicBlocks);
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
    }

    // Update is called once per frame
    void Update()
    {        
        variableStorage.TryGetValue("$entertheantique", out entertheantique);        
        variableStorage.TryGetValue("$trigger_TheAllKnowingLady", out trigger_TheAllKnowingLady);
        variableStorage.TryGetValue("$putintheinventory003", out putintheinventory003);
        variableStorage.TryGetValue("$exittheantique", out exittheantique);
        variableStorage.TryGetValue("$poetsdiary", out poetsdiary);
        variableStorage.TryGetValue("$coinisselected", out coinisselected);
        variableStorage.TryGetValue("$putintheinventory008", out putintheinventory008);
        variableStorage.TryGetValue("$showthedrawing", out showthedrawing);

        if (trigger_TheAllKnowingLady)
        {
            dialogueRunner.SaveStateToPlayerPrefs();
            transitionAnimator.SetBool("transitiontodw", true);
            if (instanceallowed03)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
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

        if (showthedrawing)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            variableStorage.SetValue("$showthedrawing", false);
        }

        if (entertheantique && !isInAntiqueShop)
        {
            // move the character to antique position and change camera thingy
            player.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(insideOfAntique.transform.position.x, insideOfAntique.transform.position.y, 0f);           
            StartCoroutine(fadeLayer.FadeOut());
            isInAntiqueShop = true;
            variableStorage.SetValue("$entertheantique", false);
            player.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
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

        if (putintheinventory003)
        {
            if (!playerInventory.myInventory.Contains(item003))
            {
                playerInventory.myInventory.Add(item003);
                Destroy(item003Object);
            }
        }

        if (putintheinventory008)
        {
            if (!playerInventory.myInventory.Contains(item008))
            {
                playerInventory.myInventory.Add(item008);
            }
        }

        if (coinisselected)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            playerInventory.myInventory.Remove(coinForFountain);
        }

        if (poetsdiary && !isIllustrationWatched.initialValue)
        {
            isIllustrationWatched.initialValue = true;
            illustrationPanel.transform.GetChild(0).GetComponent<Image>().sprite = poetsDairyImage;
            variableStorage.SetValue("$poetsdiary", false);
        }

        if (exittheantique && isInAntiqueShop)
        {
            // move the character back in front of antique
            player.transform.localScale = new Vector3(1f, 1f, 0f);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(inFrontOfAntique.transform.position.x, inFrontOfAntique.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInAntiqueShop = false;
            variableStorage.SetValue("$exittheantique", false);
            player.transform.localScale = new Vector3(1f, 1f, 0f);
            if (instanceallowed02)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_DreamWorld_003_TheBlocks_Loopable");
                    instance.start();
                    instanceallowed02 = false;
                    instanceallowed = true;
                }
        }
    }
}
