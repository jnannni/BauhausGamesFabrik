using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class StoneReaction : MonoBehaviour
{
    public SignalSend stoneSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;
    [SerializeField] private BoolValue isNearGolem;
    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "DreamWorld1" && isNearGolem.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            stoneSignal.Raise();
        }                
    }

}
