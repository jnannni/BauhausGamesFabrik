using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool putintheinventory002;
    private bool enterthechurch;
    private bool trigger_TheChurch;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        dialogueRunner.StartDialogue("TheGraveYard");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_TheChurch", out trigger_TheChurch);
        variableStorage.TryGetValue("$enterthechurch", out enterthechurch);
        variableStorage.TryGetValue("$putintheinventory002", out putintheinventory002);

        if (trigger_TheChurch)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (enterthechurch)
        {
            // move the character to the church position
        }

        if (putintheinventory002)
        {
            // put stuff in the inventory
        }
    }
}
