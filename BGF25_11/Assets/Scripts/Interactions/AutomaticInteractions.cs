using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AutomaticInteractions : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    [SerializeField] private string nameOfTheNode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueRunner.StartDialogue(nameOfTheNode);
    }
}
