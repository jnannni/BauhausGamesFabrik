using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Factory : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool trigger_TheBeach;
    private bool showthepicture;
    private bool enterthefactory;
    private bool magnoliasits;
    private bool putintheinventory11;
    private bool pettherabbit;
    private bool bringtherabbit;
    private bool lettherabbit;
    private bool rabbitunder;
    private bool faderabbit;
    private bool instanceallowed;
    private bool instanceallowed02;
    private bool instanceallowed03;
    private bool exitfactory;

    private FadeLayer fadeLayer;
    private PhysicalInvetoryItem addToInventory;
    private AudioManager audioManager;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject insideTheFactory;
    [SerializeField] private GameObject outsideOfTheFactory;
    [SerializeField] private InventoryItem item11;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;
    [SerializeField] private Sprite pictureFrameImage;
    [SerializeField] private Animator purplerabbit;
    [SerializeField] private Animator alternateAnimator;
    [SerializeField] private GameObject pictureFrame;
    [SerializeField] private GameObject rabbit;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator Fader;
    [SerializeField] private Animator CrossFade;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02; 
    private FMOD.Studio.EventInstance instance03; 
    public SpriteRenderer rabbitsprite;
    [SerializeField] private Animator transitionAnimator;

    public Transform alternateTransform;
    public Transform position;
    public Transform position02;

    private bool isInsideOfTheFactory;

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
        //audioManager.InitializeMusic(FMODEvents.instance.musicFabric);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_011_2_TheFactory_OUTDOOR_Loopable");
        instance.start();
        dialogueRunner.StartDialogue("TheFactory_Start");
        instanceallowed = true;
        instanceallowed02 = true;
        instanceallowed03 = true;
        isInsideOfTheFactory = false;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_TheBeach", out trigger_TheBeach);
        variableStorage.TryGetValue("$showthepicture", out showthepicture);
        variableStorage.TryGetValue("$enterthefactory", out enterthefactory);
        variableStorage.TryGetValue("$magnoliasits", out magnoliasits);
        variableStorage.TryGetValue("$putintheinventory11", out putintheinventory11);
        variableStorage.TryGetValue("$pettherabbit", out pettherabbit);
        variableStorage.TryGetValue("$bringtherabbit", out bringtherabbit);
        variableStorage.TryGetValue("$lettherabbit", out lettherabbit);
        variableStorage.TryGetValue("$faderabbit", out faderabbit);
        variableStorage.TryGetValue("$rabbitunder", out rabbitunder);
        variableStorage.TryGetValue("$exitfactory", out exitfactory);

        if (rabbitunder)
        {
            alternateAnimator.SetBool("holdrabbit", false);
            alternateAnimator.SetBool("walkrabbit", true);
        }

        if (trigger_TheBeach)
        {
            //instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //instance02.release();
            //SceneManager.LoadScene(nameOfTheScene);

            dialogueRunner.SaveStateToPlayerPrefs();
            transitionAnimator.SetBool("transitiontodw", true);
            if (instanceallowed03)
            {
                instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance02.release();
                instance03 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_WW_to_DW");
                instance03.start();
                instanceallowed03 = false;
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontodw"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontodw", false);
                instance03.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);;
                instance03.release();
            }
        }

        if (alternateTransform.transform.position == position.transform.position)
        {
            rabbitsprite.enabled = false;
            //dialogueRunner.StartDialogue("HoldingRabbit");
            position.transform.position = new Vector3(0, 0, 0);
        }

        if (alternateTransform.transform.position == position02.transform.position)
        {
            dialogueRunner.StartDialogue("RabbitUnderTheSun");
            position02.transform.position = new Vector3(0,0,0);
        }

        if (bringtherabbit)
        {
            playerAnimator.SetBool("dissapear", true);
            alternateAnimator.SetBool("holdrabbit", true);
            bringtherabbit = false;
        }

        if (pettherabbit)
        {
            purplerabbit.SetBool("rabbitjump", true);
        }

        if (faderabbit)
        {
            CrossFade.SetBool("Start", true);
            //Fader.SetBool("fadein", true);
        }

        if (!faderabbit)
        {
            CrossFade.SetBool("Start", false);
            //Fader.SetBool("fadein", false);
        }

        if (lettherabbit)
        {
            purplerabbit.SetBool("rabbitjump", false);
        }

        if (showthepicture && !isIllustrationWatched.initialValue)
        {
            // show the illustration
            isIllustrationWatched.initialValue = true;
            illustrationPanel.transform.GetChild(0).GetComponent<Image>().sprite = pictureFrameImage;
            variableStorage.SetValue("$showthepicture", false);
        }

        if (enterthefactory && !isInsideOfTheFactory)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(insideTheFactory.transform.position.x, insideTheFactory.transform.position.y, 0f);
            //isInsideOfTheFactory = true;
            variableStorage.SetValue("$enterthefactory", false);
            isInsideOfTheFactory = true;
            StartCoroutine(fadeLayer.FadeOut());
            //player.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_011_1_TheFactory_INDOOR_Loopable");
                instance02.start();
                instanceallowed = false;
                instanceallowed02 = true;
            }
        }

        if (putintheinventory11)
        {
            if (!playerInventory.myInventory.Contains(item11))
            {
                playerInventory.myInventory.Add(item11);
                Destroy(pictureFrame);
            }
        }

        if (exitfactory && isInsideOfTheFactory)
        {
            //player.transform.localScale = new Vector3(1f, 1f, 0f);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(outsideOfTheFactory.transform.position.x, outsideOfTheFactory.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInsideOfTheFactory = false;
            variableStorage.SetValue("$exitfactory", false);
            //player.transform.localScale = new Vector3(1f, 1f, 0f);
            if (instanceallowed02)
                {
                    instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    instance02.release();
                    instance = FMODUnity.RuntimeManager.CreateInstance("event:/Atmos/Atmo_WakingWorld_011_2_TheFactory_OUTDOOR_Loopable");
                    instance.start();
                    instanceallowed02 = false;
                    instanceallowed = true;
                }
        }
    }
}
