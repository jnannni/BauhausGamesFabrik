using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level7 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool trigger_TheDog;
    private bool putintheinventory006;
    private bool enterthemuseum;
    private bool exitthemuseum;
    private bool putintheinventory007;
    private bool takethefeather;
    private bool getclose;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private InventoryItem item006;
    [SerializeField] private InventoryItem item007;
    [SerializeField] private InventoryItem simpleFeather;
    [SerializeField] private GameObject insideOfTheMuseum;
    [SerializeField] private GameObject outsideOfTheMuseum;
    [SerializeField] private GameObject player;

    private PhysicalInvetoryItem addToInventory;
    private bool isInsideOfTheMuseum;
    private FadeLayer fadeLayer;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheMuseumCorner");
        fadeLayer = FindObjectOfType<FadeLayer>();
        isInsideOfTheMuseum = false;
        dialogueRunner.StartDialogue("TheMuseumCorner");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_TheDog", out trigger_TheDog);
        variableStorage.TryGetValue("$putintheinventory006", out putintheinventory006);
        variableStorage.TryGetValue("$enterthemuseum", out enterthemuseum);
        variableStorage.TryGetValue("$exitthemuseum", out exitthemuseum);
        variableStorage.TryGetValue("$putintheinventory007", out putintheinventory007);
        variableStorage.TryGetValue("$takethefeather", out takethefeather);
        variableStorage.TryGetValue("$getclose", out getclose);

        if (trigger_TheDog)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (putintheinventory006)
        {
            addToInventory.AddingItemFromDialogue(item006);
        }

        if (enterthemuseum && !isInsideOfTheMuseum)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = insideOfTheMuseum.transform.position;
            isInsideOfTheMuseum = true;
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (exitthemuseum && isInsideOfTheMuseum)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = outsideOfTheMuseum.transform.position;
            isInsideOfTheMuseum = false;
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (putintheinventory007)
        {
            addToInventory.AddingItemFromDialogue(item007);
        }

        if (takethefeather)
        {
            addToInventory.AddingItemFromDialogue(simpleFeather);
        }

        if (getclose)
        {
            //player.transform.position = Vector2.MoveTowards();
        }
    }
}
