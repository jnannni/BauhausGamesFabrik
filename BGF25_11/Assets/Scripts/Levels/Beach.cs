using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Beach : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool goverthebridge;
    private bool cutscene_moon;
    private bool mournersmove;
    private bool magnoliainarow;
    private bool putintheinventory12;
    private bool everythingchanges;
    private bool activated;
    private bool fadechihiro;
    private bool fadescene;

    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private InventoryItem item12;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject dustParticleSystem;
    [SerializeField] private Animator ChihiroWalk;
    [SerializeField] private Animator ChihiroCam;
    [SerializeField] private GameObject chimango;
    [SerializeField] private GameObject finalposition;
    [SerializeField] private GameObject chihiro;
    [SerializeField] private GameObject magnoliaRow;
    [SerializeField] private Animator GlobalLight;
    [SerializeField] private Animator AnimationCamera;
    [SerializeField] private Animator Fader;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoolValue isPaused;
    [SerializeField] public Animator mournersAnimator;
    [SerializeField] public Animator mournersAnimator2;
    [SerializeField] public Animator mournersAnimator3;
    [SerializeField] public Animator crossFade;
    [SerializeField] private float approachSpeed = 2f;
    public BoxCollider2D mournerBox01;
    private FMOD.Studio.EventInstance instance;

    public Transform mourner4;
    public Transform mourner3;
    public Transform mourner2;
    
    public Transform mournerPos4;
    public Transform mournerPos3;
    public Transform mournerPos2;

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
        dialogueRunner.LoadStateFromPlayerPrefs();
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_012_TheBeach_Loopable");
        instance.start();
        //audioManager.InitializeMusic(FMODEvents.instance.musicBeach);
        dialogueRunner.StartDialogue("TheBeach");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$goverthebridge", out goverthebridge);
        variableStorage.TryGetValue("$cutscene_moon", out cutscene_moon);
        variableStorage.TryGetValue("$mournersmove", out mournersmove);
        variableStorage.TryGetValue("$magnoliainarow", out magnoliainarow);
        variableStorage.TryGetValue("$putintheinventory12", out putintheinventory12);
        variableStorage.TryGetValue("$everythingchanges", out everythingchanges);
        variableStorage.TryGetValue("$activated", out activated);
        variableStorage.TryGetValue("$fadechihiro", out fadechihiro);
        variableStorage.TryGetValue("$fadescene", out fadescene);
        
        if (fadescene)
        {
            crossFade.SetBool("Start", true);
        }

        if (!fadescene)
        {
            crossFade.SetBool("Start", false);
        }

        if(magnoliainarow)
        {
            playerAnimator.SetBool("walking02", true);     
            playerAnimator.SetFloat("Horizontal", 1f);
            playerAnimator.SetFloat("Vertical", 0);
            player.transform.position = Vector3.MoveTowards(player.transform.position, magnoliaRow.transform.position, approachSpeed * Time.deltaTime);
        }

        if(!magnoliainarow)
        {
            playerAnimator.SetBool("walking02", false);
        }

        if(fadechihiro)
        {
            Fader.SetBool("fadein", true);
        }

        if(!fadechihiro)
        {
            Fader.SetBool("fadein", false);
        }

        if(goverthebridge)
        {
            Destroy(chihiro);
            playerAnimator.SetBool("dissapear", true);
            //firstCamera.enabled = false;
            //secondCamera.enabled = true;
            ChihiroWalk.SetBool("chihirogo", true);
            GlobalLight.SetBool("changeit", true);
            if(chimango.transform.position == finalposition.transform.position)
            {
               instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
               instance.release();
               SceneManager.LoadScene(nameOfTheScene);
            }
        }

        if (putintheinventory12)
        {
            if (!playerInventory.myInventory.Contains(item12))
            {
                playerInventory.myInventory.Add(item12);
            }
        }

        if (everythingchanges)
        {
            //globalLight.color = new Color(33, 27, 120, 255);
            GlobalLight.SetBool("changeit2", true);
            dustParticleSystem.SetActive(true);
            variableStorage.SetValue("$everythingchanges", false);
        }

        if (mournersmove)
        {
            mournerBox01.enabled = false;

            mournersAnimator.SetBool("theymove", true);
            mourner4.transform.position = Vector3.MoveTowards(mourner4.transform.position, mournerPos4.transform.position, approachSpeed * Time.deltaTime);
            
            mournersAnimator2.SetBool("theymove", true);
            mourner3.transform.position = Vector3.MoveTowards(mourner3.transform.position, mournerPos3.transform.position, approachSpeed * Time.deltaTime);
            
            mournersAnimator3.SetBool("theymove", true);
            mourner2.transform.position = Vector3.MoveTowards(mourner2.transform.position, mournerPos2.transform.position, approachSpeed * Time.deltaTime);
        }

        if (mourner4.transform.position == mournerPos4.transform.position)
        {
            mournersAnimator.SetBool("theymove", false);
            mournersAnimator2.SetBool("theymove", false);
            mournersAnimator3.SetBool("theymove", false);
            dialogueRunner.StartDialogue("TheMournesrs_Next");
            mournerPos4.transform.position = new Vector3(0,0,0);
        }
    }
}
