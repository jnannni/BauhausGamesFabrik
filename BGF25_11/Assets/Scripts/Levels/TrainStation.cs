using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TrainStation : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    [SerializeField] private GameObject player;
    [SerializeField] private Sprite mapImage;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private AnimationClip golemAnimationClip;
    [SerializeField] private Animator transitionAnimator;

    private bool trainapproaching;
    private bool trigger_TheFactory;
    private bool lookatthemap;
    private bool cutSceneEnded = false;
    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;

    [SerializeField] private string nameOfTheScene;    
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02; 
    private FMOD.Studio.EventInstance instance03; 

    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        dialogueRunner.LoadStateFromPlayerPrefs();
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioManager.InitializeMusic(FMODEvents.instance.musicTrainStation);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_009_TheTrainstation_Loopable");
        instance.start();
        dialogueRunner.StartDialogue("BackInTheWaking");
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trainapproaching", out trainapproaching);
        variableStorage.TryGetValue("$trigger_TheFactory", out trigger_TheFactory);
        variableStorage.TryGetValue("$lookatthemap", out lookatthemap);

        if (trigger_TheFactory)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicDW1);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToWW, this.transform.position);*/
            dialogueRunner.SaveStateToPlayerPrefs();
            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instanceallowed = false;
            }
            if (!cutSceneEnded)
            {
                if (instanceallowed02)
                {
                    instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CutScene_01_Golem");
                    instance02.start();
                    instanceallowed02 = false;
                }
                canvasAnimator.SetBool("trainon", true);                
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);
            }            
            if (canvasAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("traincutscene"))
            {
                canvasAnimator.SetBool("trainon", false);
                cutSceneEnded = true;
                transitionAnimator.SetBool("transitiontoww", true);  
                if (instanceallowed03)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance03 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_WW_to_WW");
                    instance03.start();
                    instanceallowed03 = false;
                }                              
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                instance03.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance03.release();
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);
                trigger_TheFactory = false;
            }
        }

        if (lookatthemap && !isIllustrationWatched.initialValue)
        {
            isIllustrationWatched.initialValue = true;
            illustrationPanel.transform.GetChild(0).GetComponent<Image>().sprite = mapImage;
            variableStorage.SetValue("$lookatthemap", false);
        }
    }
}
