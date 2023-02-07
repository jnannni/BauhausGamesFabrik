using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level7 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool trigger_TheDog;
    private bool putintheinventory006;
    private bool enterthemuseum;
    private bool exitthemuseum;
    private bool putintheinventory007;
    private bool enterthegarden;
    private bool exitthegarden;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheMuseumCorner");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_TheDog", out trigger_TheDog);
        variableStorage.TryGetValue("$tputintheinventory006", out putintheinventory006);
        variableStorage.TryGetValue("$enterthemuseum", out enterthemuseum);
        variableStorage.TryGetValue("$exitthemuseum", out exitthemuseum);
        variableStorage.TryGetValue("$putintheinventory007", out putintheinventory007);
        variableStorage.TryGetValue("$tenterthegarden", out enterthegarden);
        variableStorage.TryGetValue("$exitthegarden", out exitthegarden);
    }
}
