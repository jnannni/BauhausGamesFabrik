using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class WakingWorld : MonoBehaviour
{
    // to be able to go to the next level following booleans should be true
    // godownthestairs
    // takethekey - immidiate transition to the dream world
    // next booleans are used to control the environment
    // gobackhome - transition the player to default spot (home)
    // curtainopens - delete the obstical on the player's way
    //
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private GameObject homePosition;
    [SerializeField] private GameObject upTheStairsPosition;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator CurtainAnimator;
    private GameObject curtains;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02;
    
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool takethekey;
    private bool godownthestairs;
    private bool gobackhome;
    private bool goupthestairs003;
    private bool curtainopens;
    private bool curtainanimation;
    private bool instanceallowed;
    private bool instanceallowed02;
    
    private bool isDownTheStairs;
    private bool wentHome;

    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    [SerializeField] private InventoryItem key;    
    [SerializeField] private PlayerInventory playerInventory;

    [SerializeField] private BoxCollider2D curtainCollider;
    [SerializeField] private BoxCollider2D curtainCollider02;

    private void Awake()
    {        
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        PlayerPrefs.DeleteAll();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        //audioManager.InitializeMusic(FMODEvents.instance.musicWW1);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_001_002_TheUphillTheDownhill_Loopable");
        instance.start();
        dialogueRunner.StartDialogue("FirstScene");
        isDownTheStairs = false;
        wentHome = false;
        curtains = GameObject.FindWithTag("TheCurtains");
        instanceallowed = true;
        instanceallowed02 = true;
    }

    // Update is called once per frame
    void Update()
    {        
        variableStorage.TryGetValue("$takethekey", out takethekey);
        variableStorage.TryGetValue("$godownthestairs", out godownthestairs);
        variableStorage.TryGetValue("$gobackhome", out gobackhome);
        variableStorage.TryGetValue("$curtainopens", out curtainopens);
        variableStorage.TryGetValue("$goupthestairs003", out goupthestairs003);
        variableStorage.TryGetValue("$curtainanimation", out curtainanimation);

        if (curtains && curtainopens)
        {
            curtainCollider.enabled = false;
            curtainCollider02.enabled = false;
            //StartCoroutine(fadeLayer.FadeIn());
            //Destroy(curtains);
            //StartCoroutine(fadeLayer.FadeOut());
        }

        if (godownthestairs && !isDownTheStairs)
        {            
            //audioManager.StopASoundWithFade(FMODEvents.instance.musicWW1);
            StartCoroutine(fadeLayer.FadeIn());
            GoDownTheStairs();            
            isDownTheStairs = true;
            variableStorage.SetValue("$godownthestairs", false);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (goupthestairs003)
        {
            StartCoroutine(fadeLayer.FadeIn());
            GoUpTheStairs();
            isDownTheStairs = false;
            variableStorage.SetValue("$goupthestairs003", false);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (takethekey)
        {
            dialogueRunner.SaveStateToPlayerPrefs();
            if (!playerInventory.myInventory.Contains(key))
            {
                playerInventory.myInventory.Add(key);                 
            }
            //audioManager.PlayOneShot(FMODEvents.instance.transitionToDW, this.transform.position);
            transitionAnimator.SetBool("transitiontodw", true);
            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_WW_to_DW");
                instance02.start();
                instanceallowed = false;
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontodw"))
            {
                instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);;
                instance02.release();
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene("DreamWorld1");                
                transitionAnimator.SetBool("transitiontodw", false);
            }            
        }

        if (gobackhome && !wentHome)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(homePosition.transform.position.x, homePosition.transform.position.y, 0f);            
            wentHome = true;
            variableStorage.SetValue("$gobackhome", false);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (curtainanimation)
        {
            CurtainAnimator.SetBool("curtainno", true);
        }
        
        if (!curtainanimation)
        {
            CurtainAnimator.SetBool("curtainno", false);
        }
    }

    void GoDownTheStairs()
    {
        player.transform.position = new Vector3(targetPosition.transform.position.x, targetPosition.transform.position.y, 0f);        
    }

    void GoUpTheStairs()
    {
        // add isdownthesrairs in yarn
        player.transform.position = new Vector3(upTheStairsPosition.transform.position.x, upTheStairsPosition.transform.position.y, 0f);
    }
}
