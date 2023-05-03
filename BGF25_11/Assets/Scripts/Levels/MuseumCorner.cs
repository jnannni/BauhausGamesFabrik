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
    [SerializeField] private GameObject closerToDog;
    [SerializeField] private GameObject theDog;
    [SerializeField] private float approachSpeed = 1f;
    [SerializeField] private PlayerInventory playerInventory;
    
    private bool isInsideOfTheMuseum;
    private FadeLayer fadeLayer;
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
        isInsideOfTheMuseum = false;
        audioManager.InitializeMusic(FMODEvents.instance.musicMuseumCorner);
        dialogueRunner.StartDialogue("TheMuseumCorner");
        dialogueRunner.LoadStateFromPlayerPrefs();
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
            player.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(insideOfTheMuseum.transform.position.x, insideOfTheMuseum.transform.position.y, 0f);           
            StartCoroutine(fadeLayer.FadeOut());
            isInsideOfTheMuseum = true;
            variableStorage.SetValue("$exitthemuseum", false);
            player.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
            
        }

        if (exitthemuseum && isInsideOfTheMuseum)
        {
            player.transform.localScale = new Vector3(1f, 1f, 0f);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(outsideOfTheMuseum.transform.position.x, outsideOfTheMuseum.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInsideOfTheMuseum = false;
            variableStorage.SetValue("$enterthemuseum", false);
            player.transform.localScale = new Vector3(1f, 1f, 0f);
        }

        if (takethefeather)
        {
            if (!playerInventory.myInventory.Contains(simpleFeather))
            {
                playerInventory.myInventory.Add(simpleFeather);
            }
        }

        if (getclose && player.transform.position != closerToDog.transform.position)
        {    
            playerAnimator.SetBool("walking02", true);     
            playerAnimator.SetFloat("Horizontal", 1f);
            playerAnimator.SetFloat("Vertical", 0);
            player.transform.position = Vector3.MoveTowards(player.transform.position, closerToDog.transform.position, approachSpeed * Time.deltaTime);
            Destroy(theDog);
        }

         if(player.transform.position == closerToDog.transform.position)
        {
            getclose = false; 
            playerAnimator.SetBool("walking02", false);            
        }
    }
}
