using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Graveyard : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    public Animator scaredCatAnimator;
    public Animator transitionAnimator;

    private bool putintheinventory002;
    private bool enterthechurch;
    private bool trigger_TheChurch;
    private bool scurryawayanimation;
    private bool exitthechurch;

    private AudioManager audioManager;
    
    private bool isInsideTheChurch;
    private FadeLayer fadeLayer;    
    [SerializeField] private InventoryItem item002;
    [SerializeField] private GameObject item002Object;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject catCreature;
    [SerializeField] private GameObject insideTheChurch;
    [SerializeField] private GameObject outsideTheChurch;
    [SerializeField] private PlayerInventory playerInventory;    
    private float distanceToCat;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        dialogueRunner.LoadStateFromPlayerPrefs();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.InitializeMusic(FMODEvents.instance.musicGraveyard);
        isInsideTheChurch = false;
        dialogueRunner.StartDialogue("TheGraveYard");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToCat = Vector2.Distance(player.transform.position, catCreature.transform.position);
        variableStorage.TryGetValue("$trigger_TheChurch", out trigger_TheChurch);
        variableStorage.TryGetValue("$scurryawayanimation", out scurryawayanimation);                
        variableStorage.TryGetValue("$putintheinventory002", out putintheinventory002);
        variableStorage.TryGetValue("$enterthechurch", out enterthechurch);
        variableStorage.TryGetValue("$exitthechurch", out exitthechurch);

        if (scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", true);
        } else if (!scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", false);
        }

        if (trigger_TheChurch)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicGraveyard);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToDW, this.transform.position);*/
            dialogueRunner.SaveStateToPlayerPrefs();
            player.transform.localScale = new Vector3(1f, 1f, player.transform.localScale.z);
            transitionAnimator.SetBool("transitiontodw", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontodw"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontodw", false);
            }                        
        }

        if (enterthechurch && !isInsideTheChurch)
        {
            // move the character to the church position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(insideTheChurch.transform.position.x, insideTheChurch.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInsideTheChurch = true;
            variableStorage.SetValue("$enterthechurch", false);
            player.transform.localScale = new Vector3(1.4f, 1.4f, 0f);
        }

        if (exitthechurch && isInsideTheChurch)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(outsideTheChurch.transform.position.x, outsideTheChurch.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInsideTheChurch = false;
            variableStorage.SetValue("$exitthechurch", false);
            player.transform.localScale = new Vector3(1f, 1f, 0f);
        }

        if (putintheinventory002)
        {
            if (!playerInventory.myInventory.Contains(item002))
            {
                playerInventory.myInventory.Add(item002);
                item002Object.GetComponent<ObjectInteractable>().enabled = false;
            }
        }        

        if (distanceToCat > 5)
        {
            variableStorage.SetValue("$scurryawayanimation", false);
        }
    }
}
