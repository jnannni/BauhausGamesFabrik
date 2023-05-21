using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class FrozenVillage : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    [SerializeField] private GameObject player;

    private bool tryflamingsword;
    private bool jar_used;
    private bool camerashaking;
    private bool magnoliawalksforward;
    private bool priestshows;
    private bool trigger_waking06;
    private bool isonice;
    private bool isonice02;
    private bool crossfade;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator candleStickAnimator;
    [SerializeField] private GameObject inventoryPanel;
    public BoolValue isInventoryOpen;
    public BoolValue isPaused;
    [SerializeField] private Animator transitionAnimator;
    public SpriteRenderer OutCharacter;
    [SerializeField] private GameObject IcePosition;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] public BoxCollider2D box01;
    [SerializeField] public BoxCollider2D box02;

    public OnGroundInteractable fromScript;
    public OnGroundInteractable fromScript01;
    public OnGroundInteractable fromScript02;
    public OnGroundInteractable fromScript03;
    public OnGroundInteractable fromScript04;
    public OnGroundInteractable fromScript05;
    public OnGroundInteractable fromScript06;
    public OnGroundInteractable fromScript07;
    public PolygonCollider2D bigonice;

    public SpriteRenderer magnoliasshadow;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02;
    private bool instanceallowed;
    private bool instanceallowed02;
    

    private FadeLayer fadeLayer;
    private AudioManager audioManager;
    public Animator CrossFade;


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
        //audioManager.InitializeMusic(FMODEvents.instance.musicFrozenVillage);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_DreamWorld_004_TheFrozenVillage_Loopable");
        instance.start();
        dialogueRunner.StartDialogue("TheDreamworld_04");
        bigonice.enabled = false;
        instanceallowed = true;
        instanceallowed02 = true;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$tryflamingsword", out tryflamingsword);
        variableStorage.TryGetValue("$jar_used", out jar_used);
        variableStorage.TryGetValue("$camerashaking", out camerashaking);
        variableStorage.TryGetValue("$magnoliawalksforward", out magnoliawalksforward);
        variableStorage.TryGetValue("$priestshows", out priestshows);
        variableStorage.TryGetValue("$trigger_waking06", out trigger_waking06);
        variableStorage.TryGetValue("$isonice", out isonice);
        variableStorage.TryGetValue("$isonice02", out isonice02);
        variableStorage.TryGetValue("$crossfade", out crossfade);

        if (camerashaking || !camerashaking)
        {
            cameraAnimator.SetBool("shake", camerashaking);
        }

        if (jar_used)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            candleStickAnimator.SetBool("isLit", true);
        }

        if (trigger_waking06)
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
                trigger_waking06 = false;
            }            
        }

        if (priestshows)
        {
            OutCharacter.enabled = true;
        }

        if (isonice)
        {
            playerAnimator.SetBool("flying", true);
            box01.enabled = false;
            box02.enabled = false;
            bigonice.enabled = true;
            player.transform.position = new Vector3(-6.57f, -34.22f, 0f);
            magnoliasshadow.enabled = false;
        }

        if (isonice02)
        {
            playerAnimator.SetBool("flying", false);
            box01.enabled = true;
            box02.enabled = true;
            bigonice.enabled = false;
            magnoliasshadow.enabled = true;
            //player.transform.position = new Vector3(fromScript.positionSpawn.position.x, fromScript.positionSpawn.position.y, 0f);
            //player.transform.position = new Vector3(-6.57f, -39.17f, 0f);
        }

        if (crossfade)
        {
            CrossFade.SetBool("Start", true);
        }

        if (!crossfade)
        {
            CrossFade.SetBool("Start", false);
        }

        if (!priestshows)
        {
            OutCharacter.enabled = false;
        }
    }
}
