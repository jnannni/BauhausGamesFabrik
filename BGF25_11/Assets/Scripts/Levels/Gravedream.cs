using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Gravedream : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private FadeLayer fadeLayer;
    private Dissolve dissolve;
    private float fade = 1f;
    private bool isDissolved = false;

    private bool trigger_waking03;
    private bool hedge_cleared;
    private bool pickuptheobject;

    private AudioManager audioManager;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private GameObject hedgeObject;
    [SerializeField] private InventoryItem ticketItem;
    [SerializeField] private GameObject ticketObject;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private BoolValue isInventoryOpen;
    [SerializeField] private BoolValue isPaused;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private PlayerInventory playerInventory;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.InitializeMusic(FMODEvents.instance.musicGravedream);
        dialogueRunner.StartDialogue("TheDreamworld_02");
        dissolve = FindObjectOfType<Dissolve>();
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_waking03", out trigger_waking03);
        variableStorage.TryGetValue("$hedge_cleared", out hedge_cleared);
        variableStorage.TryGetValue("$pickuptheobject", out pickuptheobject);

        if (trigger_waking03)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicGravedream);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToWW, this.transform.position);*/
            transitionAnimator.SetBool("transitiontoww", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);                
            }
        }

        if (pickuptheobject)
        {
            if (!playerInventory.myInventory.Contains(ticketItem))
            {
                playerInventory.myInventory.Add(ticketItem);
                ticketObject.GetComponent<ObjectInteractable>().enabled = false;
            }
        }

        if (hedge_cleared)
        {                        
            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
                isInventoryOpen.initialValue = false;
                isPaused.initialValue = false;
            }
            Destroy(hedgeObject);
            // destroy hedge
        }
    }
}
