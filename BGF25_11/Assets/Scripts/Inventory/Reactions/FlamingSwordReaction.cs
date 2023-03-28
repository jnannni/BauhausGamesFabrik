using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class FlamingSwordReaction : MonoBehaviour
{
    public SignalSend flamingSwordSignal;
    private DialogueRunner dialogueRunner;
    [SerializeField] private BoolValue isNearHedge;
    public string startNode;        

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "Gravedream" && isNearHedge.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            flamingSwordSignal.Raise();
        }
    }
}
