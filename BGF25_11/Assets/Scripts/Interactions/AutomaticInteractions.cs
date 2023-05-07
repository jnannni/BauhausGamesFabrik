using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AutomaticInteractions : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    [SerializeField] private string nameOfTheNode;
    [SerializeField] private bool oneUse;
    public bool camerashake;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        camerashake = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueRunner.StartDialogue(nameOfTheNode);
        camerashake = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        camerashake = false;
    }
}
