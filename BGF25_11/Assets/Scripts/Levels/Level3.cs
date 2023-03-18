using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject downTheStairsPosition;
    [SerializeField] private GameObject homePosition;
    [SerializeField] private InventoryItem item001;
    [SerializeField] private InventoryItem itemd001;
    private PhysicalInvetoryItem addToInventory;
    private bool gobackhome;
    private bool magnoliadances;
    private bool gotothegraveyard;
    private bool lookatthedrawing;
    private bool putintheinventory001;
    private bool putintheinventoryd001;
    private bool godownthestairs;
    private FadeLayer fadeLayer;
    private bool isDownTheStairs;

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
        dialogueRunner.StartDialogue("BackInTheWaking");
        isDownTheStairs = false;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$gobackhome", out gobackhome);
        variableStorage.TryGetValue("$magnoliadances", out magnoliadances);
        variableStorage.TryGetValue("$gotothegraveyard", out gotothegraveyard);
        variableStorage.TryGetValue("$glookatthedrawing", out lookatthedrawing);
        variableStorage.TryGetValue("$putintheinventory001", out putintheinventory001);
        variableStorage.TryGetValue("$putintheinventoryd001", out putintheinventoryd001);
        variableStorage.TryGetValue("$godownthestairs", out godownthestairs);

        if (magnoliadances)
        {
            // add animation to animator for the dance
        }

        if (lookatthedrawing)
        {
            // show the picture of the drawing or move camera to show it
        }

        if (putintheinventory001)
        {
            addToInventory.AddingItemFromDialogue(item001);
        }

        if (putintheinventoryd001)
        {
            addToInventory.AddingItemFromDialogue(itemd001);
        }

        if (gotothegraveyard)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (godownthestairs && !isDownTheStairs)
        {
            StartCoroutine(fadeLayer.FadeIn());
            GoDownTheStairs();
            isDownTheStairs = true;
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (gobackhome)
        {
            // move character to the home position (add fade in/out to yarn)
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = homePosition.transform.position;
            StartCoroutine(fadeLayer.FadeOut());
        }

    }

    void GoDownTheStairs()
    {
        player.transform.position = downTheStairsPosition.transform.position;
    }

    void GoUpTheStairs()
    {
        // add isdownthesrairs in yarn
        player.transform.position = downTheStairsPosition.transform.position;
    }
}
