using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level5 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;    

    private bool entertheantique;
    private bool trigger_TheAllKnowingLady;
    private bool putintheinventory003;
    private bool exittheantique;
    private bool poetsdiary;
    private bool putintheinventory008;
    private bool isInAntiqueShop;
    private FadeLayer fadeLayer;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject inFrontOfAntique;
    [SerializeField] private GameObject insideOfAntique;
    [SerializeField] private InventoryItem item003;
    [SerializeField] private InventoryItem item008;
    private PhysicalInvetoryItem addToInventory;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInAntiqueShop = false;
        dialogueRunner.StartDialogue("TheBlocks");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$entertheantique", out entertheantique);
        variableStorage.TryGetValue("$trigger_TheAllKnowingLady", out trigger_TheAllKnowingLady);
        variableStorage.TryGetValue("$putintheinventory003", out putintheinventory003);
        variableStorage.TryGetValue("$exittheantique", out exittheantique);
        variableStorage.TryGetValue("$poetsdiary", out poetsdiary);
        variableStorage.TryGetValue("$putintheinventory008", out putintheinventory008);

        if (trigger_TheAllKnowingLady)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (entertheantique && !isInAntiqueShop)
        {
            // move the character to antique position and change camera thingy
            player.transform.localScale = new Vector3(1.7f, 1.7f, player.transform.localScale.z);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = insideOfAntique.transform.position;           
            StartCoroutine(fadeLayer.FadeOut());
            isInAntiqueShop = true;
            variableStorage.SetValue("$exittheantique", false);
            player.transform.localScale = new Vector3(1.7f, 1.7f, player.transform.localScale.z);
        }

        if (putintheinventory003)
        {
            // put stuff in the inventory
            addToInventory.AddingItemFromDialogue(item003);
        }

        if (putintheinventory008)
        {
            addToInventory.AddingItemFromDialogue(item008);
        }

        if (poetsdiary)
        {
            // show illustration
        }

        if (exittheantique && isInAntiqueShop)
        {
            // move the character back in front of antique
            player.transform.localScale = new Vector3(1f, 1f, player.transform.localScale.z);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = inFrontOfAntique.transform.position;
            StartCoroutine(fadeLayer.FadeOut());
            isInAntiqueShop = false;
            variableStorage.SetValue("$entertheantique", false);
            player.transform.localScale = new Vector3(1f, 1f, player.transform.localScale.z);
        }
    }
}
