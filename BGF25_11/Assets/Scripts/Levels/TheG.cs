using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TheG : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool putintheinventory004;
    private bool putintheinventory005;
    private bool goontheroof;
    private bool lookaround;
    private bool entertheschool01;
    private bool entertheschool02;
    private bool enterthegranshouse;
    private bool godowntheroof;
    private bool gototheMuseum;
    private bool playscreamsound;
    private bool sheisdancing;
    private bool pulsingeffect;
    private bool fallingfeeling;

    private FadeLayer fadeLayer;
    private bool isOnTheRoof;
    private Renderer stairsRenderer;
    private Renderer grandpasHouseRenderer;
    private BoxCollider2D[] boxCollider2Ds;
    private BoxCollider2D boxCollider2DFall;
    private PhysicalInvetoryItem addToInventory;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject roofStairs;
    [SerializeField] private GameObject roofFall;
    [SerializeField] private GameObject grandpasHouse;
    [SerializeField] private GameObject behindTheSchool;
    [SerializeField] private GameObject grandpasHouseInside;
    [SerializeField] private GameObject onTheRoof;
    [SerializeField] private GameObject downTheRoof;
    [SerializeField] private InventoryItem item004;
    [SerializeField] private InventoryItem item005;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
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
        variableStorage.TryGetValue("$putintheinventory004", out putintheinventory004);
        variableStorage.TryGetValue("$putintheinventory005", out putintheinventory005);
        variableStorage.TryGetValue("$goontheroof", out goontheroof);
        variableStorage.TryGetValue("$lookaround", out lookaround);
        variableStorage.TryGetValue("$entertheschool01", out entertheschool01);
        variableStorage.TryGetValue("$entertheschool01", out entertheschool02);
        variableStorage.TryGetValue("$enterthegranshouse", out enterthegranshouse);
        variableStorage.TryGetValue("$godowntheroof", out godowntheroof);
        variableStorage.TryGetValue("$gototheMuseum", out gototheMuseum);
        variableStorage.TryGetValue("$playscreamsound", out playscreamsound);
        variableStorage.TryGetValue("$sheisdancing", out sheisdancing);
        variableStorage.TryGetValue("$pulsingeffect", out pulsingeffect);
        variableStorage.TryGetValue("$fallingfeeling", out fallingfeeling);

        if (gototheMuseum)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (putintheinventory004)
        {
            // put stuff in the inventory
            addToInventory.AddingItemFromDialogue(item004);
        }

        if (putintheinventory005)
        {
            // put stuff in the inventory
            addToInventory.AddingItemFromDialogue(item005);
        }

        if (goontheroof && !isOnTheRoof)
        {
            // move character to the roof position
            player.transform.localScale = new Vector3(1f, 1f, player.transform.localScale.z);
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = onTheRoof.transform.position;
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

        if (lookaround)
        {
            // trigger a cut scene
        }

        if (entertheschool01 || entertheschool02)
        {
            // move character to school position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = behindTheSchool.transform.position;
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (enterthegranshouse)
        {
            // move character to grans position
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = grandpasHouseInside.transform.position;
            StartCoroutine(fadeLayer.FadeOut());
            player.transform.localScale = new Vector3(1.5f, 1.5f, player.transform.localScale.z);
        }

        if (godowntheroof && isOnTheRoof)
        {
            // move character to infront of the roof
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = downTheRoof.transform.position;
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
