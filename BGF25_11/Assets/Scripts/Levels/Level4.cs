using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    public Animator scaredCatAnimator;

    private bool putintheinventory002;
    private bool enterthechurch;
    private bool trigger_TheChurch;
    private bool scurryawayanimation;
    private bool searchforanitem;
    private bool isInsideTheChurch;
    private FadeLayer fadeLayer;
    private PhysicalInvetoryItem addToInventory;
    [SerializeField] private InventoryItem item002;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject catCreature;
    [SerializeField] private GameObject insideTheChurch;
    private float distanceToCat;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInsideTheChurch = false;
        dialogueRunner.StartDialogue("TheGraveYard");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToCat = Vector2.Distance(player.transform.position, catCreature.transform.position);
        variableStorage.TryGetValue("$trigger_TheChurch", out trigger_TheChurch);
        variableStorage.TryGetValue("$scurryawayanimation", out scurryawayanimation);        
        variableStorage.TryGetValue("$searchforanitem", out searchforanitem);
        variableStorage.TryGetValue("$putintheinventory002", out putintheinventory002);
        variableStorage.TryGetValue("$enterthechurch", out enterthechurch);

        if (scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", true);
        } else if (!scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", false);
        }

        if (trigger_TheChurch)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (enterthechurch && !isInsideTheChurch)
        {
            // move the character to the church position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = insideTheChurch.transform.position;
            StartCoroutine(fadeLayer.FadeOut());
            isInsideTheChurch = true;
        }

        if (putintheinventory002)
        {
            // put stuff in the inventory
            addToInventory.AddingItemFromDialogue(item002);
        }

        if (distanceToCat > 5)
        {
            variableStorage.SetValue("$scurryawayanimation", false);
        }
    }
}
