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
    [SerializeField] private Animator GlobalLight;
    [SerializeField] private Animator AnimationCamera;
    [SerializeField] private Animator Fader;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoolValue isPaused;

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
        audioManager.InitializeMusic(FMODEvents.instance.musicBeach);
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
        
        
        if(fadechihiro)
        {
            Fader.SetBool("fadein", true);
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
            globalLight.color = new Color(185, 49, 100, 1);
            dustParticleSystem.SetActive(true);
            variableStorage.SetValue("$everythingchanges", false);
        }
    }
}
