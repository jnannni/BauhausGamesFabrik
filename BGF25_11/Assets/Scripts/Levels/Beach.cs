using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Beach : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool goverthebridge;
    private bool cutscene_moon;
    private bool mournersmove;
    private bool magnoliainarow;
    private bool putintheinventory12;
    private bool everythingchanges;

    private FadeLayer fadeLayer;
    private PhysicalInvetoryItem addToInventory;

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;    
    [SerializeField] private InventoryItem item12;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheBeach");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$goverthebridge", out goverthebridge);
        variableStorage.TryGetValue("$cutscene_moon", out cutscene_moon);
        variableStorage.TryGetValue("$mournersmove", out mournersmove);
        variableStorage.TryGetValue("$magnoliainarow", out magnoliainarow);
        variableStorage.TryGetValue("$putintheinventory12", out putintheinventory12);
        variableStorage.TryGetValue("$everythingchanges", out everythingchanges);

        if(goverthebridge)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (putintheinventory12)
        {
            addToInventory.AddingItemFromDialogue(item12);
        }
    }
}
