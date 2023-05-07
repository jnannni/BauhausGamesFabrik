using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class GoldenCupReaction : MonoBehaviour
{
    public SignalSend cupSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;
    [SerializeField] private BoolValue isNearSnakes;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "RedBeach" && isNearSnakes.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            cupSignal.Raise();
        }
    }
}
