using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class FrozenVillage : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    [SerializeField] private GameObject player;

    private bool tryflamingsword;
    private bool jar_used;
    private bool camerashaking;
    private bool magnoliawalksforward;
    private bool priestshows;
    private bool trigger_waking06;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator candleStickAnimator;
    [SerializeField] private GameObject inventoryPanel;
    public BoolValue isInventoryOpen;
    public BoolValue isPaused;
    [SerializeField] private Animator transitionAnimator;

    private FadeLayer fadeLayer;
    private AudioManager audioManager;


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
        audioManager.InitializeMusic(FMODEvents.instance.musicFrozenVillage);
        dialogueRunner.StartDialogue("TheDreamworld_04");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$tryflamingsword", out tryflamingsword);
        variableStorage.TryGetValue("$jar_used", out jar_used);
        variableStorage.TryGetValue("$camerashaking", out camerashaking);
        variableStorage.TryGetValue("$magnoliawalksforward", out magnoliawalksforward);
        variableStorage.TryGetValue("$priestshows", out priestshows);
        variableStorage.TryGetValue("$trigger_waking06", out trigger_waking06);

        if (camerashaking || !camerashaking)
        {
            cameraAnimator.SetBool("shake", camerashaking);
        }

        if (jar_used)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            candleStickAnimator.SetBool("isLit", true);
        }

        if (trigger_waking06)
        {
            transitionAnimator.SetBool("transitiontoww", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);
                trigger_waking06 = false;
            }            
        }
    }
}
