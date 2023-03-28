using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class DreamWorld01  : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private AnimationClip golemAnimationClip;
    [SerializeField] private BoolValue isInventoryAvailable;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject inventoryPanel;
    public BoolValue isInventoryOpen;
    public BoolValue isPaused;
    public GameObject appearSpotPosition;
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    private FadeLayer fadeLayer;
    private bool cutSceneEnded = false;
    private bool openInventory;
    private bool trigger_waking02;
    private bool standup;
    private bool laying;
    private bool stoneSelected;
    private int iteration = 0;

    private void Awake()
    {
        //player.transform.position = appearSpotPosition.transform.position;
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheDreamworld_01");
    }

    // Update is called once per frame
    void Update()
    {        
        variableStorage.TryGetValue("$openInventory", out openInventory);
        variableStorage.TryGetValue("$trigger_waking02", out trigger_waking02);
        variableStorage.TryGetValue("$standup", out standup);
        variableStorage.TryGetValue("$laying", out laying);
        variableStorage.TryGetValue("$stoneSelected", out stoneSelected);

        isInventoryAvailable.initialValue = openInventory;

        if (openInventory && iteration == 0)
        {
            isInventoryOpen.initialValue = true;
            inventoryPanel.SetActive(true);
            iteration++;
        }

        if (stoneSelected)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
        }

        if (trigger_waking02)
        {
            if (!cutSceneEnded)
            {
                canvasAnimator.SetBool("startCutSceneGolem", true);                
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
                trigger_waking02 = false;
            }
        }

        if (laying)
        {
            playerAnimator.SetBool("laying", true);
        }

        if (standup)
        {
            playerAnimator.SetBool("laying", false);
            playerAnimator.SetBool("standup", true);
        }

        if (!standup)
        {
            playerAnimator.SetBool("standup", false);
        }
    }   
}
