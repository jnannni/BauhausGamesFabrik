using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool gobackhome;
    private bool magnoliadances;
    private bool gotothegraveyard;
    private bool lookatthedrawing;
    private bool putintheinventory001;
    private bool putintheinventoryd001;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("BackInTheWaking");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$gobackhome", out gobackhome);
        variableStorage.TryGetValue("$magnoliadances", out magnoliadances);
        variableStorage.TryGetValue("$gotothegraveyard", out gotothegraveyard);
        variableStorage.TryGetValue("$glookatthedrawing", out lookatthedrawing);
        variableStorage.TryGetValue("$putintheinventory001", out putintheinventory001);
        variableStorage.TryGetValue("$putintheinventoryd001", out putintheinventoryd001);

        if (magnoliadances)
        {
            // add animation to animator for the dance
        }

        if (lookatthedrawing)
        {
            // show the picture of the drawing or move camera to show it
        }

        if (putintheinventory001)
        {
            // put stuff in the inventory
        }

        if (putintheinventoryd001)
        {
            // put stuff in the inventory
        }

        if (gotothegraveyard)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (gobackhome)
        {
            // move character to the home position (add fade in/out to yarn)
        }

    }
}
