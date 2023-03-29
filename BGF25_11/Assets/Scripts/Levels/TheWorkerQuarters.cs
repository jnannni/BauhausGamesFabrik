using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TheWorkerQuarters : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool loopthemap;
    private bool theworkercomesout;
    private bool cutscene_rabbits;
    private bool loopthetext;
    private bool trigger_Frozen;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private BoolValue isLoopTriggered;
    [SerializeField] private BoolValue changeCameraSmoothing;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator transitionAnimator;

    private Vector3 playerStartPosition;
    private Vector3 playerCurrentPosition;
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
        dialogueRunner.StartDialogue("TheWorkerQuarters");        
        playerCurrentPosition = player.transform.position;
        playerStartPosition = playerPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$theworkercomesout", out theworkercomesout);
        variableStorage.TryGetValue("$cutscene_rabbits", out cutscene_rabbits);
        variableStorage.TryGetValue("$trigger_Frozen", out trigger_Frozen);

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
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontodw"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontodw", false);
            }
        }
    }    
}
