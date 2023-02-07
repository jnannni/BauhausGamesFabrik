using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level5 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool entertheantique;
    private bool trigger_TheAllKnowingLady;
    private bool putintheinventory003;
    private bool exittheantique;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheBlocks");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$entertheantique", out entertheantique);
        variableStorage.TryGetValue("$trigger_TheAllKnowingLady", out trigger_TheAllKnowingLady);
        variableStorage.TryGetValue("$putintheinventory003", out putintheinventory003);
        variableStorage.TryGetValue("$exittheantique", out exittheantique);

        if (trigger_TheAllKnowingLady)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (entertheantique)
        {
            // move the character to antique position
        }

        if (putintheinventory003)
        {
            // put stuff in the inventory
        }

        if (exittheantique)
        {
            // move the character back in front of antique
        }
    }
}
