using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TheG : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    public Animator transitionAnimator;

    private BoxCollider2D[] boxCollider2Ds;
    private BoxCollider2D boxCollider2DFall;

    private Renderer stairsRenderer;
    private Renderer grandpasHouseRenderer;

    private bool enterthegranshouse;
    private bool putintheinventory004; 
    private bool putintheinventory005; 
    private bool goontheroof; 
    private bool lookaround;
    private bool entertheschool01; 
    private bool entertheschool02;  
    private bool godowntheroof; 
    private bool gototheMuseum; 
    private bool playscreamsound;
    private bool sheisdancing;
    private bool pulsingeffect; 
    private bool fallingfeeling;
    private bool exitgrandpashouse;
    private bool movemagnoliaback;
    private bool gavethegriefbook;
    private bool gavethestrangebook;
    private bool objectfromsecurity;

    private AudioManager audioManager;
    
    private bool isInGrandpaHouse;
    private bool isOnTheRoof;
    private bool isBehinfTheSchool;
    private FadeLayer fadeLayer;    
    [SerializeField] private GameObject grandpasHouseInside;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject roofStairs;
    [SerializeField] private GameObject roofFall;
    [SerializeField] private GameObject grandpasHouse;
    [SerializeField] private GameObject behindTheSchool;
    [SerializeField] private GameObject inFrontOfTheSchool;
    [SerializeField] private GameObject grandpasHouseOutside;
    [SerializeField] private GameObject onTheRoof;
    [SerializeField] private GameObject downTheRoof;
    [SerializeField] private InventoryItem item004;
    [SerializeField] private GameObject item004Object;
    [SerializeField] private InventoryItem item005;
    [SerializeField] private GameObject item005Object;
    [SerializeField] private InventoryItem griefingBook;
    [SerializeField] private InventoryItem strangeBook;
    [SerializeField] private InventoryItem objectFromSecurity;
    [SerializeField] private PlayerInventory playerInventory; 
    [SerializeField] private GameObject inventoryPanel; 
    [SerializeField] private BoolValue isInventoryOpen;
    [SerializeField] private BoolValue isPaused;  
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator canvasAnimator;
    public AutomaticInteractions takingbool;
    private bool cutSceneEnded = false;

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
        audioManager.InitializeMusic(FMODEvents.instance.musicTheG);
        dialogueRunner.StartDialogue("TheG");
        dialogueRunner.LoadStateFromPlayerPrefs();
        boxCollider2Ds = roofStairs.GetComponents<BoxCollider2D>();
        boxCollider2DFall = roofFall.GetComponent<BoxCollider2D>();
        stairsRenderer = roofStairs.GetComponent<Renderer>();
        grandpasHouseRenderer = grandpasHouse.GetComponent<Renderer>();
        isBehinfTheSchool = false;
        isInGrandpaHouse = false;
        isOnTheRoof = false;
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$enterthegranshouse", out enterthegranshouse);
        variableStorage.TryGetValue("$putintheinventory004", out putintheinventory004); 
        variableStorage.TryGetValue("$putintheinventory005", out putintheinventory005); 
        variableStorage.TryGetValue("$goontheroof", out goontheroof); 
        variableStorage.TryGetValue("$lookaround", out lookaround);
        variableStorage.TryGetValue("$entertheschool01", out entertheschool01); 
        variableStorage.TryGetValue("$entertheschool01", out entertheschool02); 
        variableStorage.TryGetValue("$godowntheroof", out godowntheroof); 
        variableStorage.TryGetValue("$gototheMuseum", out gototheMuseum); 
        variableStorage.TryGetValue("$playscreamsound", out playscreamsound);
        variableStorage.TryGetValue("$sheisdancing", out sheisdancing);
        variableStorage.TryGetValue("$pulsingeffect", out pulsingeffect);
        variableStorage.TryGetValue("$fallingfeeling", out fallingfeeling);
        variableStorage.TryGetValue("$gavethegriefbook", out gavethegriefbook);
        variableStorage.TryGetValue("$gavethestrangebook", out gavethestrangebook);
        variableStorage.TryGetValue("$objectfromsecurity", out objectfromsecurity);
        variableStorage.TryGetValue("$exitgrandpashouse", out exitgrandpashouse);

        if (putintheinventory004)
        {
            if (!playerInventory.myInventory.Contains(item004))
            {
                playerInventory.myInventory.Add(item004);
                Destroy(item004Object);
            }
        }

        if (putintheinventory005)
        {
            if (!playerInventory.myInventory.Contains(item005))
            {
                playerInventory.myInventory.Add(item005);
                Destroy(item005Object);
            }
        }

        if (enterthegranshouse && !isInGrandpaHouse)
        {
            //audioManager.InitializeMusic(FMODEvents.instance.musicChurch);
            // move the character to the church position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(grandpasHouseInside.transform.position.x, grandpasHouseInside.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInGrandpaHouse = true;
            variableStorage.SetValue("$enterthegranshouse", false);
            player.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
        }

        if (gototheMuseum)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicTheG);
            audioManager.PlayOneShot(FMODEvents.instance.transitionBetweenWW, this.transform.position);*/
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (objectfromsecurity)
        {
            if (!playerInventory.myInventory.Contains(objectFromSecurity))
            {
                playerInventory.myInventory.Add(objectFromSecurity);                
            }
        }

        if (gavethegriefbook)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            if (playerInventory.myInventory.Contains(griefingBook))
            {
                playerInventory.myInventory.Remove(griefingBook);
            }
            variableStorage.SetValue("$gavethegriefbook", false);
        }

        if (gavethestrangebook)
        {
            inventoryPanel.SetActive(false);
            isInventoryOpen.initialValue = false;
            isPaused.initialValue = false;
            if (playerInventory.myInventory.Contains(strangeBook))
            {
                playerInventory.myInventory.Remove(strangeBook);
            }
            variableStorage.SetValue("$gavethestrangebook", false);
        }

        if (lookaround)
        {
            canvasAnimator.SetBool("lookingaround", true);                
        }

        if (!lookaround)
        {
            canvasAnimator.SetBool("lookingaround", false);
        }

        if ((entertheschool01 || entertheschool02) && !isBehinfTheSchool)
        {
            // move character to school position
            StartCoroutine(fadeLayer.FadeIn());
            isBehinfTheSchool = true;
            variableStorage.SetValue("$entertheschool01", false);
            variableStorage.SetValue("$entertheschool02", false);
            player.transform.position = new Vector3(behindTheSchool.transform.position.x, behindTheSchool.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            /*if (entertheschool01)
            {
                if (!playerInventory.myInventory.Contains(objectFromSecurity))
                {
                    playerInventory.myInventory.Add(objectFromSecurity);
                }
            }*/
        }

        if (movemagnoliaback && isBehinfTheSchool)
        {
            StartCoroutine(fadeLayer.FadeIn());
            isBehinfTheSchool = false;
            variableStorage.SetValue("$movemagnoliaback", false);            
            player.transform.position = new Vector3(inFrontOfTheSchool.transform.position.x, inFrontOfTheSchool.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (exitgrandpashouse && isInGrandpaHouse)
        {
            player.transform.localScale = new Vector3(1f, 1f, 0f);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(grandpasHouseOutside.transform.position.x, grandpasHouseOutside.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            isInGrandpaHouse = false;
            variableStorage.SetValue("$enterthegranshouse", false);
            player.transform.localScale = new Vector3(1f, 1f, 0f);
        }

        if (pulsingeffect || !pulsingeffect)
        {
            cameraAnimator.SetBool("headpulsing", pulsingeffect);
            canvasAnimator.SetBool("pulsing", pulsingeffect);
        }

        if (goontheroof && !isOnTheRoof)
        {
            // move character to the roof position
            player.transform.localScale = new Vector3(1f, 1f, 0);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(onTheRoof.transform.position.x, onTheRoof.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            variableStorage.SetValue("$godowntheroof", false);
            isOnTheRoof = true;            
            stairsRenderer.sortingLayerName = "background";
            grandpasHouseRenderer.sortingLayerName = "background";
            foreach (BoxCollider2D boxCollider2D in boxCollider2Ds)
            {
                boxCollider2D.enabled = true;
            }
            boxCollider2DFall.enabled = true;

        }

        if (godowntheroof && isOnTheRoof)
        {
            // move character to infront of the roof
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(downTheRoof.transform.position.x, downTheRoof.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            variableStorage.SetValue("$goontheroof", false);
            isOnTheRoof = false;
            stairsRenderer.sortingLayerName = "foreground";
            grandpasHouseRenderer.sortingLayerName = "Default";
            foreach (BoxCollider2D boxCollider2D in boxCollider2Ds)
            {
                boxCollider2D.enabled = false;
            }
            boxCollider2DFall.enabled = false;
        }

        if (takingbool.camerashake)
        {
            cameraAnimator.SetBool("groundshaking", true);
        }   

        if (!takingbool.camerashake)
        {
            cameraAnimator.SetBool("groundshaking", false);
        }    


    }

}
