using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class WildflowersReaction : MonoBehaviour
{
    public SignalSend wildFlowersSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;
    [SerializeField] private BoolValue isNearWildflowers;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "Beach" && isNearWildflowers.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();                        
            dialogueRunner.StartDialogue(startNode);
            wildFlowersSignal.Raise();
        }
    }
}
