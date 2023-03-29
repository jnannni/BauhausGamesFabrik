using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TrainTicketReaction : MonoBehaviour
{
    public SignalSend ticketSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;
    [SerializeField] private BoolValue isNearOldPerson;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "TrainStation" && isNearOldPerson.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            ticketSignal.Raise();
        }
    }
}
