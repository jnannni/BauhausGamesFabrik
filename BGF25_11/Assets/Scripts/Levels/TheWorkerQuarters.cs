using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TheWorkerQuarters : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool cutSceneEnded = false;
    private bool loopthemap;
    private bool theworkercomesout;
    private bool cutscene_rabbits;
    private bool loopthetext;
    private bool trigger_Frozen;
    private bool stoprabbit;


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private BoolValue isLoopTriggered;
    [SerializeField] private BoolValue changeCameraSmoothing;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator theworkerAnimator;
    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;

    private Vector3 playerStartPosition;
    private Vector3 playerCurrentPosition;
    private FadeLayer fadeLayer;
    private AudioManager audioManager;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private AnimationClip RabbitAnimationClip;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02; 
    private FMOD.Studio.EventInstance instance03;

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
        //audioManager.InitializeMusic(FMODEvents.instance.musicWorkerQuarters);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_010_TheWorkerquarters_Loopable");
        instance.start();
        dialogueRunner.StartDialogue("TheWorkerQuarters");        
        playerCurrentPosition = player.transform.position;
        playerStartPosition = playerPosition.transform.position;
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$theworkercomesout", out theworkercomesout);
        variableStorage.TryGetValue("$cutscene_rabbits", out cutscene_rabbits);
        variableStorage.TryGetValue("$trigger_Frozen", out trigger_Frozen);
        variableStorage.TryGetValue("$stoprabbit", out stoprabbit);

        playerCurrentPosition = player.transform.position;
        playerStartPosition = new Vector3(playerPosition.transform.position.x, playerCurrentPosition.y, 0f);

        if (isLoopTriggered.initialValue)
        {
            player.transform.position = playerStartPosition;            
            cam.smoothing = 1f;
            isLoopTriggered.initialValue = false;
        }

        if (changeCameraSmoothing.initialValue)
        {
            cam.smoothing = 0.1f;
            changeCameraSmoothing.initialValue = false;            
        }

        if (trigger_Frozen)
        {
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
            }
        }

        if (theworkercomesout || !theworkercomesout)
        {
            theworkerAnimator.SetBool("isOut", theworkercomesout);
        }

        if (cutscene_rabbits)
        {
            if (!cutSceneEnded)
            {
                canvasAnimator.SetBool("startCutSceneRabbit", true);
                if (instanceallowed)
                {
                    instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance.release();
                    instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CutScene_02_RabbitBag");
                    instance02.start();
                    instanceallowed = false;
                    instanceallowed02 = true;
                }
            
                
                
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);    
            }            
        }

        if (stoprabbit)
        {
            canvasAnimator.SetBool("startCutSceneRabbit", false);
            if (instanceallowed02)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_010_TheWorkerquarters_Loopable");
                    instance.start();
                    instanceallowed02 = false;
                    instanceallowed02 = true;
                }
            cutSceneEnded = true;
        }

    }    
}
