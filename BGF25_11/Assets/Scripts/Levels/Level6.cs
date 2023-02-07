using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level6 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool putintheinventory004;
    private bool putintheinventory005;
    private bool goontheroof;
    private bool lookaround;
    private bool entertheschool;
    private bool enterthegranshouse;
    private bool godowntheroof;
    private bool gototheMuseum;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheG");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$putintheinventory004", out putintheinventory004);
        variableStorage.TryGetValue("$putintheinventory005", out putintheinventory005);
        variableStorage.TryGetValue("$goontheroof", out goontheroof);
        variableStorage.TryGetValue("$lookaround", out lookaround);
        variableStorage.TryGetValue("$entertheschool", out entertheschool);
        variableStorage.TryGetValue("$enterthegranshouse", out enterthegranshouse);
        variableStorage.TryGetValue("$godowntheroof", out godowntheroof);
        variableStorage.TryGetValue("$gototheMuseum", out gototheMuseum);

        if (gototheMuseum)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (putintheinventory004)
        {
            // put stuff in the inventory
        }

        if (putintheinventory005)
        {
            // put stuff in the inventory
        }

        if (goontheroof)
        {
            // move character to the roof position
        }

        if (lookaround)
        {
            // trigger a cut scene
        }

        if (entertheschool)
        {
            // move character to school position
        }

        if (enterthegranshouse)
        {
            // move character to grans position
        }

        if (godowntheroof)
        {
            // move character to infront of the roof
        }

        if (gototheMuseum)
        {
            // move character to the museum
        }
    }
}
