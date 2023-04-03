using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Factory : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool trigger_TheBeach;
    private bool showthepicture;
    private bool enterthefactory;
    private bool magnoliasits;
    private bool putintheinventory11;
    private bool pettherabbit;
    private bool bringtherabbit;

    private FadeLayer fadeLayer;
    private PhysicalInvetoryItem addToInventory;
    private AudioManager audioManager;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject insideTheFactory;
    [SerializeField] private InventoryItem item11;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;
    [SerializeField] private Sprite pictureFrameImage;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.InitializeMusic(FMODEvents.instance.musicFabric);
        dialogueRunner.StartDialogue("TheFactory_Start");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_TheBeach", out trigger_TheBeach);
        variableStorage.TryGetValue("$showthepicture", out showthepicture);
        variableStorage.TryGetValue("$enterthefactory", out enterthefactory);
        variableStorage.TryGetValue("$magnoliasits", out magnoliasits);
        variableStorage.TryGetValue("$putintheinventory11", out putintheinventory11);
        variableStorage.TryGetValue("$pettherabbit", out pettherabbit);
        variableStorage.TryGetValue("$bringtherabbit", out bringtherabbit);

        if (trigger_TheBeach)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (showthepicture && !isIllustrationWatched.initialValue)
        {
            // show the illustration
            isIllustrationWatched.initialValue = true;
            illustrationPanel.transform.GetChild(0).GetComponent<Image>().sprite = pictureFrameImage;
            variableStorage.SetValue("$showthepicture", false);
        }

        if (enterthefactory)
        {
            player.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
        }

        if (putintheinventory11)
        {
            if (!playerInventory.myInventory.Contains(item11))
            {
                playerInventory.myInventory.Add(item11);
            }
        }
    }
}
