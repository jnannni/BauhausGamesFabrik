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

    [SerializeField] private string nameOfTheScene;
    [SerializeField] private GameObject player;
    [SerializeField] private Light globalLight;
    [SerializeField] private InventoryItem item12;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameObject dustParticleSystem;

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
            if (!playerInventory.myInventory.Contains(item12))
            {
                playerInventory.myInventory.Add(item12);
            }
        }

        if (everythingchanges)
        {
            globalLight.color = new Color(185, 49, 100, 1);
            dustParticleSystem.SetActive(true);
            variableStorage.SetValue("$everythingchanges", false);
        }
    }
}
