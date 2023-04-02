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
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private InventoryItem item006;    
    [SerializeField] private InventoryItem simpleFeather;
    [SerializeField] private GameObject insideOfTheMuseum;
    [SerializeField] private GameObject outsideOfTheMuseum;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerInventory playerInventory;
    
    private bool isInsideOfTheMuseum;
    private FadeLayer fadeLayer;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.InitializeMusic(FMODEvents.instance.musicMuseumCorner);
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
            if (!playerInventory.myInventory.Contains(item006))
            {
                playerInventory.myInventory.Add(item006);
            }
        }

        if (enterthemuseum && !isInsideOfTheMuseum)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(insideOfTheMuseum.transform.position.x, insideOfTheMuseum.transform.position.y, 0f);
            isInsideOfTheMuseum = true;
            variableStorage.SetValue("$enterthemuseum", false);
            StartCoroutine(fadeLayer.FadeOut());
            player.transform.localScale = new Vector3(1.8f, 1.8f, 0f);
        }

        if (exitthemuseum && isInsideOfTheMuseum)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(outsideOfTheMuseum.transform.position.x, outsideOfTheMuseum.transform.position.y, 0f);
            isInsideOfTheMuseum = false;
            variableStorage.SetValue("$exitthemuseum", false);
            StartCoroutine(fadeLayer.FadeOut());
            player.transform.localScale = new Vector3(1f, 1f, 0f);
        }

        if (takethefeather)
        {
            if (!playerInventory.myInventory.Contains(simpleFeather))
            {
                playerInventory.myInventory.Add(simpleFeather);
            }
        }

        if (getclose)
        {            
            //playerAnimator.SetFloat("Horizontal", -1f);
            //playerAnimator.SetFloat("Vertical", 0);
            //player.transform.position = Vector3.MoveTowards(player.transform.position, closeToParents.transform.position, approachSpeed * Time.deltaTime);
        }
    }
}
