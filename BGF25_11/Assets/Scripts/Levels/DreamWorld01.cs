using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class DreamWorld01  : MonoBehaviour
{
    [SerializeField] private GameObject player;    
    [SerializeField] private BoolValue isInventoryAvailable;
    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject inventoryPanel;
    public BoolValue isInventoryOpen;
    public GameObject appearSpotPosition;
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    private FadeLayer fadeLayer;
    private bool openInventory;
    private bool triggerback;
    private bool triggerAnimation;
    private int iteration = 0;

    private void Awake()
    {
        player.transform.position = appearSpotPosition.transform.position;
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("SecondScene");
    }

    // Update is called once per frame
    void Update()
    {        
        variableStorage.TryGetValue("$openInventory", out openInventory);
        variableStorage.TryGetValue("$triggerback", out triggerback);
        variableStorage.TryGetValue("$triggerAnimation", out triggerAnimation);

        isInventoryAvailable.initialValue = openInventory;

        if (openInventory && iteration == 0)
        {
            isInventoryOpen.initialValue = true;
            inventoryPanel.SetActive(true);
            iteration++;
        }

        if (triggerback)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }
    }    
}
