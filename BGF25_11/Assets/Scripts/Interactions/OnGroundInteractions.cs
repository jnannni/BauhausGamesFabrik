using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OnGroundInteractions : MonoBehaviour
{
    private DialogueRunner dialogueRunner;


    //[SerializeField] private string nameOfTheNode;
    [SerializeField] private bool oneUse;
    [SerializeField] GameObject iceClue;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //dialogueRunner.StartDialogue(nameOfTheNode);
        iceClue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        iceClue.SetActive(false);
    }
}
