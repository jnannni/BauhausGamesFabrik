using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Level1 : MonoBehaviour
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
    private GameObject curtains;   
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    private bool takethekey;
    private bool godownthestairs;
    private bool gobackhome;
    private bool curtainopens;
    public BoolValue isLevel1Completed;    
    private bool isDownTheStairs;
    private bool wentHome;
    private FadeLayer fadeLayer;    

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        dialogueRunner.StartDialogue("FirstScene");
            isDownTheStairs = false;
            wentHome = false;
       
        curtains = GameObject.FindWithTag("TheCurtains");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$takethekey", out takethekey);
        variableStorage.TryGetValue("$godownthestairs", out godownthestairs);
        variableStorage.TryGetValue("$gobackhome", out gobackhome);
        variableStorage.TryGetValue("$curtainopens", out curtainopens);

        if (curtains && curtainopens)
        {
            Destroy(curtains);
        }

        if (godownthestairs && !isDownTheStairs)
        {
            StartCoroutine(fadeLayer.FadeIn());
            GoDownTheStairs();            
            isDownTheStairs = true;
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (takethekey)
        {
            isLevel1Completed.initialValue = true;
        }

        if (gobackhome && !wentHome)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = homePosition.transform.position;
            wentHome = true;
            StartCoroutine(fadeLayer.FadeOut());
        }
    }

    void GoDownTheStairs()
    {
        player.transform.position = targetPosition.transform.position;        
    }
    
}
