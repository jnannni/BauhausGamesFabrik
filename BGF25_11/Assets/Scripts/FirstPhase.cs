using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FirstPhase : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private bool goDownTheStairs;
    private static bool isAtHome;
    public string thoughtsStartNode;
    private InMemoryVariableStorage variableStorage;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        variableStorage.TryGetValue("$godownthestairs", out goDownTheStairs);
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            isAtHome = true;
            dialogueRunner.StartDialogue(thoughtsStartNode);
            Debug.Log("at home " + isAtHome);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            isAtHome = false;
            Debug.Log("not home " + isAtHome);
        }
    }


    [YarnFunction("get_bool")]
    public static bool GetBool()
    {
        return isAtHome;
    }
    // Start is called before the first frame update
    void Start()
    {
        isAtHome = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
