using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AutomaticInteractions02 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    [SerializeField] private string nameOfTheNode;
    [SerializeField] private bool oneUse;
    public bool enteredbox;

    private void Awake()
    {
        enteredbox = false;
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enteredbox = true;
        //dialogueRunner.StartDialogue(nameOfTheNode);
    }
}
