using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TheG : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool putintheinventory004; //
    private bool putintheinventory005; //
    private bool goontheroof; //
    private bool lookaround;
    private bool entertheschool01; //
    private bool entertheschool02; //
    private bool enterthegranshouse; //
    private bool godowntheroof; //
    private bool gototheMuseum; //
    private bool playscreamsound;
    private bool sheisdancing;
    private bool pulsingeffect; //
    private bool fallingfeeling;
    private bool exitgrandpashouse;
    private bool movemagnoliaback;
    private bool gavethegriefbook;
    private bool gavethestrangebook;
    private bool objectfromsecurity;

    private FadeLayer fadeLayer;
    private bool isOnTheRoof;
    private bool isInGrandpaHouse;
    private bool isBehinfTheSchool;
    private Renderer stairsRenderer;
    private Renderer grandpasHouseRenderer;
    private BoxCollider2D[] boxCollider2Ds;
    private BoxCollider2D boxCollider2DFall;    

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject roofStairs;
    [SerializeField] private GameObject roofFall;
    [SerializeField] private GameObject grandpasHouse;
    [SerializeField] private GameObject behindTheSchool;
    [SerializeField] private GameObject inFrontOfTheSchool;
    [SerializeField] private GameObject grandpasHouseInside;
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
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private GameObject inventoryPanel;
    public BoolValue isInventoryOpen;
    public BoolValue isPaused;

    private AudioManager audioManager;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.InitializeMusic(FMODEvents.instance.musicTheG);
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheG");
        boxCollider2Ds = roofStairs.GetComponents<BoxCollider2D>();
        boxCollider2DFall = roofFall.GetComponent<BoxCollider2D>();
        stairsRenderer = roofStairs.GetComponent<Renderer>();
        grandpasHouseRenderer = grandpasHouse.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$putintheinventory004", out putintheinventory004); //
        variableStorage.TryGetValue("$putintheinventory005", out putintheinventory005); //
        variableStorage.TryGetValue("$goontheroof", out goontheroof); //
        variableStorage.TryGetValue("$lookaround", out lookaround);
        variableStorage.TryGetValue("$entertheschool01", out entertheschool01); //
        variableStorage.TryGetValue("$entertheschool01", out entertheschool02); //
        variableStorage.TryGetValue("$enterthegranshouse", out enterthegranshouse); //
        variableStorage.TryGetValue("$godowntheroof", out godowntheroof); //
        variableStorage.TryGetValue("$gototheMuseum", out gototheMuseum); //
        variableStorage.TryGetValue("$playscreamsound", out playscreamsound);
        variableStorage.TryGetValue("$sheisdancing", out sheisdancing);
        variableStorage.TryGetValue("$pulsingeffect", out pulsingeffect);
        variableStorage.TryGetValue("$fallingfeeling", out fallingfeeling);
        variableStorage.TryGetValue("$gavethegriefbook", out gavethegriefbook);
        variableStorage.TryGetValue("$gavethestrangebook", out gavethestrangebook);
        variableStorage.TryGetValue("$objectfromsecurity", out objectfromsecurity);

        if (gototheMuseum)
        {
            /*audioManager.PauseMusic(FMODEvents.instance.musicTheG);
            audioManager.PlayOneShot(FMODEvents.instance.transitionBetweenWW, this.transform.position);*/
            SceneManager.LoadScene(nameOfTheScene);
        }

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

        if (objectfromsecurity)
        {
            if (!playerInventory.myInventory.Contains(objectFromSecurity))
            {
                playerInventory.myInventory.Add(objectFromSecurity);                
            }
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
            // trigger a cut scene
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

        if (enterthegranshouse && !isInGrandpaHouse)
        {
            // move character to grans position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(grandpasHouseInside.transform.position.x, grandpasHouseInside.transform.position.y, 0f);
            isInGrandpaHouse = true;
            variableStorage.SetValue("$enterthegranshouse", false);
            StartCoroutine(fadeLayer.FadeOut());
            player.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
        }

        if (exitgrandpashouse && isInGrandpaHouse)
        {
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(grandpasHouseOutside.transform.position.x, grandpasHouseOutside.transform.position.y, 0f);
            isInGrandpaHouse = false;
            variableStorage.SetValue("$exitgrandpashouse", false);
            StartCoroutine(fadeLayer.FadeOut());
            player.transform.localScale = new Vector3(1f, 1f, 0f);
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
    }
}
