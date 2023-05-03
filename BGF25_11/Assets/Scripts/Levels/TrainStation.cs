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

    [SerializeField] private string nameOfTheScene;    
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;

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
        audioManager.InitializeMusic(FMODEvents.instance.musicTrainStation);
        dialogueRunner.StartDialogue("BackInTheWaking");
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
            if (!cutSceneEnded)
            {
                canvasAnimator.SetBool("startCutSceneGolem", true);                
                //audioManager.InitializeMusic(FMODEvents.instance.golemCutScene);
            }            
            if (canvasAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && canvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("golemDialogue"))
            {
                canvasAnimator.SetBool("startCutSceneGolem", false);
                cutSceneEnded = true;
                transitionAnimator.SetBool("transitiontoww", true);                                
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
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
