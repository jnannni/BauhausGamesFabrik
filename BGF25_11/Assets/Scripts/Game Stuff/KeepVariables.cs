using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class KeepVariables : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private Dictionary<string, bool> boolVariables = new Dictionary<string, bool> {
        { "$hasClueA", false }
    };
    private Dictionary<string, float> floatVariables = new Dictionary<string, float> {};
    private Dictionary<string, string> stringVariables = new Dictionary<string, string> { };

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = GameObject.FindObjectOfType<DialogueRunner>();
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        dialogueRunner.AddCommandHandler("load_variables", LoadVariables);
        dialogueRunner.AddCommandHandler("store_variables", StoreVariables);
    }

    private void LoadVariables()
    {
        variableStorage.SetAllVariables(
            floatVariables,
            stringVariables,
            boolVariables);
    }

    private void StoreVariables()
    {
        var varDicts = variableStorage.GetAllVariables();
        boolVariables = varDicts.Item3;
    }
}
