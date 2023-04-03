using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;


public class DreamBlocks : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private FadeLayer fadeLayer;
    private BoxCollider2D[] boxCollider2Ds;

    private bool trigger_waking04;
    private bool approaching_parents;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parents;
    [SerializeField] private GameObject closeToParents;
    [SerializeField] private GameObject secondPartSpot;
    [SerializeField] private BoolValue isMaskUsed;
    [SerializeField] private float approachSpeed = 2f;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private BoolValue isInventoryOpen;
    [SerializeField] private BoolValue isPaused;

    private AudioManager audioManager;

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
        audioManager.InitializeMusic(FMODEvents.instance.musicDreamBlocks);
        boxCollider2Ds = parents.GetComponents<BoxCollider2D>();        
        dialogueRunner.StartDialogue("TheDreamworld_03");
        dialogueRunner.LoadStateFromPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$approaching_parents", out approaching_parents);
        variableStorage.TryGetValue("$trigger_waking04", out trigger_waking04);

        if (trigger_waking04)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicDreamBlocks);
            audioManager.PlayOneShot(FMODEvents.instance.transitionToWW, this.transform.position);*/
            dialogueRunner.SaveStateToPlayerPrefs();
            transitionAnimator.SetBool("transitiontoww", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene(nameOfTheScene);
                transitionAnimator.SetBool("transitiontoww", false);
            }
        }

        if (isMaskUsed.initialValue)
        {
            StartCoroutine(fadeLayer.FadeIn());
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            isMaskUsed.initialValue = false;
            player.transform.position = new Vector3(secondPartSpot.transform.position.x, secondPartSpot.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());

        }

        if (approaching_parents && player.transform.position != closeToParents.transform.position)
        {
            playerAnimator.SetFloat("Horizontal", -1f);
            playerAnimator.SetFloat("Vertical", 0);
            player.transform.position = Vector3.MoveTowards(player.transform.position, closeToParents.transform.position, approachSpeed * Time.deltaTime);
            boxCollider2Ds[0].enabled = false;
            boxCollider2Ds[1].enabled = true;
            variableStorage.SetValue("$approaching_parents", false);
        }

        if(player.transform.position == closeToParents.transform.position)
        {
            approaching_parents = false;            
        }
    }
}
