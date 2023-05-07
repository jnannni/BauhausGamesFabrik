using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AmusementPark : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    private bool activated;
    private bool fadechihiro;
    private bool magnoliaisback;
    private bool startanimation;
    private bool holdthestar;
    private bool cutSceneEnded;
    private bool cutSceneEnded02;
    private bool cutSceneEnded03;
    private bool notholding;
    private bool cutscenewrong;
    private bool cutsceneright;

    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    [SerializeField] private GameObject MagnoliaAway;
    [SerializeField] private GameObject ChihiroAway;
    [SerializeField] private Animator chihiroAnimator;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private string nameOfTheScene02;
    [SerializeField] private GameObject player;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private InventoryItem item12;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject dustParticleSystem;
    [SerializeField] private Animator ChihiroWalk;
    [SerializeField] private Animator ChihiroCam;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject chimango;
    [SerializeField] private GameObject final02position;
    [SerializeField] private GameObject chihiro;
    [SerializeField] private Animator GlobalLight;
    [SerializeField] private Animator AnimationCamera;
    [SerializeField] private Animator Fader;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator canvasAnimator;
    public Transform rabbitend;
    public Transform cameraTransform;
    private bool complicated;
    [SerializeField] private Animator cameranchor;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        complicated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.InitializeMusic(FMODEvents.instance.musicBeach);
        dialogueRunner.StartDialogue("TheDreamworld_06");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$activated", out activated);
        variableStorage.TryGetValue("$fadechihiro", out fadechihiro);
        variableStorage.TryGetValue("$magnoliaisback", out magnoliaisback);
        variableStorage.TryGetValue("$startanimation", out startanimation);
        variableStorage.TryGetValue("$holdthestar", out holdthestar);
        variableStorage.TryGetValue("$notholding", out notholding);
        variableStorage.TryGetValue("$cutscenewrong", out cutscenewrong);
        variableStorage.TryGetValue("$cutsceneright", out cutsceneright);
        
        if (cameraTransform.transform.position == rabbitend.position)
        {
            complicated = true;
            cameranchor.enabled = !cameranchor.enabled;
            //rabbitend.transform.position = new Vector4(transform.position.x, 0, transform.position.z);
        }

        if(magnoliaisback)
        {
            //Fader.SetBool("fadein", true);
            player.transform.position = new Vector3(MagnoliaAway.transform.position.x, MagnoliaAway.transform.position.y, 0f);
            chihiro.transform.position = new Vector3(ChihiroAway.transform.position.x, ChihiroAway.transform.position.y, 0f); 
            //playerAnimator.SetBool("dissapear", false);
            //chihiroAnimator.SetBool("dissapearchi", false);
        }

        if(!startanimation && activated)
        {
            Fader.SetBool("fadein", true);
            //playerAnimator.SetBool("dissapear", false);
            //chihiroAnimator.SetBool("dissapearchi", false);
        }

        if(startanimation)
        {
            ChihiroWalk.SetBool("chihirogo", true);
            
            if(chimango.transform.position == final02position.transform.position && activated)
            {
               dialogueRunner.StartDialogue("AfterAnimation_Start");  
            }
        }

        if(holdthestar)
        {
            if (!cutSceneEnded)
            {
                canvasAnimator.SetBool("startCutSceneStar", true);                
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);
            }
        }

        if(notholding)
        {
            //canvasAnimator.SetBool("startCutSceneStar", false);
            cutSceneEnded = true;
        }

        if(cutsceneright)
        {
            dialogueRunner.SaveStateToPlayerPrefs();
            if (!cutSceneEnded03)
            {
                //instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CutScene_01_Golem");
                //instance02.start();
                canvasAnimator.SetBool("startCutSceneRight", true);                
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);
            }            
            if (canvasAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("goodend"))
            {
                canvasAnimator.SetBool("startCutSceneRight", false);
                cutSceneEnded03 = true;
                transitionAnimator.SetBool("transitiontoww", true);                                
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);
                cutsceneright = false;
            }          
        }
        
        if(cutscenewrong)
        {
            dialogueRunner.SaveStateToPlayerPrefs();
            if (!cutSceneEnded02)
            {
                //instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CutScene_01_Golem");
                //instance02.start();
                canvasAnimator.SetBool("startCutSceneWrong", true);                
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);
            }            
            if (canvasAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("badend"))
            {
                canvasAnimator.SetBool("startCutSceneWrong", false);
                cutSceneEnded02 = true;
                transitionAnimator.SetBool("transitiontoww", true);                                
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene02);
                transitionAnimator.SetBool("transitiontoww", false);
                cutsceneright = false;
            } 
        }

    }
}
