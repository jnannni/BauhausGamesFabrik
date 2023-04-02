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
    private GameObject curtains;
    
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool takethekey;
    private bool godownthestairs;
    private bool gobackhome;
    private bool goupthestairs003;
    private bool curtainopens;
    
    private bool isDownTheStairs;
    private bool wentHome;

    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    [SerializeField] private InventoryItem key;    
    [SerializeField] private PlayerInventory playerInventory;

    private void Awake()
    {        
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        audioManager.InitializeMusic(FMODEvents.instance.musicWW1);
        dialogueRunner.StartDialogue("FirstScene");
        isDownTheStairs = false;
        wentHome = false;
        fadeLayer = FindObjectOfType<FadeLayer>();
        curtains = GameObject.FindWithTag("TheCurtains");
    }

    // Update is called once per frame
    void Update()
    {        
        variableStorage.TryGetValue("$takethekey", out takethekey);
        variableStorage.TryGetValue("$godownthestairs", out godownthestairs);
        variableStorage.TryGetValue("$gobackhome", out gobackhome);
        variableStorage.TryGetValue("$curtainopens", out curtainopens);
        variableStorage.TryGetValue("$goupthestairs003", out goupthestairs003);

        if (curtains && curtainopens)
        {
            StartCoroutine(fadeLayer.FadeIn());
            Destroy(curtains);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (godownthestairs && !isDownTheStairs)
        {            
            audioManager.StopASoundWithFade(FMODEvents.instance.musicWW1);
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
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontodw"))
            {
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
