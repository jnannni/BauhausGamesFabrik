using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class MuseumCorner : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool goinsidethestation;
    private bool putintheinventory006;
    private bool enterthemuseum;
    private bool exitthemuseum;    
    private bool takethefeather;
    private bool getclose;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private InventoryItem item006;    
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
        variableStorage.TryGetValue("$goinsidethestation", out goinsidethestation);
        variableStorage.TryGetValue("$putintheinventory006", out putintheinventory006);
        variableStorage.TryGetValue("$enterthemuseum", out enterthemuseum);
        variableStorage.TryGetValue("$exitthemuseum", out exitthemuseum);        
        variableStorage.TryGetValue("$takethefeather", out takethefeather);
        variableStorage.TryGetValue("$getclose", out getclose);

        if (goinsidethestation)
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
            player.transform.localScale = new Vector3(1.8f, 1.8f, player.transform.localScale.z);
        }

        if (exitthemuseum && isInsideOfTheMuseum)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = outsideOfTheMuseum.transform.position;
            isInsideOfTheMuseum = false;
            StartCoroutine(fadeLayer.FadeOut());
            player.transform.localScale = new Vector3(1f, 1f, player.transform.localScale.z);
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
