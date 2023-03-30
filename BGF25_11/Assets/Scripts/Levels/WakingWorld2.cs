using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WakingWorld2 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject downTheStairsPosition;
    [SerializeField] private GameObject upTheStairsPosition;
    [SerializeField] private GameObject homePosition;
    [SerializeField] private Sprite childsDrawing;
    [SerializeField] private InventoryItem item001;
    [SerializeField] private GameObject item001Object;
    [SerializeField] private InventoryItem itemd001;
    [SerializeField] private PlayerInventory playerInventory;    

    private bool gobackhome;
    private bool magnoliadances;
    private bool gotothegraveyard;
    private bool lookatthedrawing;
    private bool putintheinventory001;
    private bool putintheinventoryd001;
    private bool godownthestairs;
    private bool goupthestairs002;
    private bool headpulsing;
    private FadeLayer fadeLayer;
    private bool isDownTheStairs;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;

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
        variableStorage.TryGetValue("$lookatthedrawing", out lookatthedrawing);
        variableStorage.TryGetValue("$putintheinventory001", out putintheinventory001);
        variableStorage.TryGetValue("$putintheinventoryd001", out putintheinventoryd001);
        variableStorage.TryGetValue("$godownthestairs", out godownthestairs);
        variableStorage.TryGetValue("$goupthestairs002", out goupthestairs002);
        variableStorage.TryGetValue("$headpulsing", out headpulsing);

        if (magnoliadances || !magnoliadances)
        {
            playerAnimator.SetBool("isDancing", magnoliadances);
        }

        if (lookatthedrawing && !isIllustrationWatched.initialValue)
        {
            // show the picture of the drawing or move camera to show it
            isIllustrationWatched.initialValue = true;
            illustrationPanel.transform.GetChild(0).GetComponent<Image>().sprite = childsDrawing;
            variableStorage.SetValue("$lookatthedrawing", false);
        }

        if (putintheinventory001)
        {
            if (!playerInventory.myInventory.Contains(item001))
            {
                playerInventory.myInventory.Add(item001);
                Destroy(item001Object);
            }
        }

        if (putintheinventoryd001)
        {
            if (!playerInventory.myInventory.Contains(itemd001))
            {
                playerInventory.myInventory.Add(itemd001);                
            }                       
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
            variableStorage.SetValue("$godownthestairs", false);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (goupthestairs002 && isDownTheStairs)
        {
            StartCoroutine(fadeLayer.FadeIn());
            GoUpTheStairs();
            isDownTheStairs = false;
            variableStorage.SetValue("$goupthestairs002", false);
            StartCoroutine(fadeLayer.FadeOut());
        }

        if (headpulsing || !headpulsing)
        {
            cameraAnimator.SetBool("headpulsing", headpulsing);
            canvasAnimator.SetBool("pulsing", headpulsing);
        }

        if (gobackhome)
        {
            // move character to the home position (add fade in/out to yarn)
            StartCoroutine(fadeLayer.FadeIn());
            player.transform.position = new Vector3(homePosition.transform.position.x, homePosition.transform.position.y, 0f);
            StartCoroutine(fadeLayer.FadeOut());
            variableStorage.SetValue("$gobackhome", false);
        }

    }

    void GoDownTheStairs()
    {
        player.transform.position = new Vector3(downTheStairsPosition.transform.position.x, downTheStairsPosition.transform.position.y, 0f);
    }

    void GoUpTheStairs()
    {
        // add isdownthesrairs in yarn
        player.transform.position = new Vector3(upTheStairsPosition.transform.position.x, upTheStairsPosition.transform.position.y, 0f);
    }
}
