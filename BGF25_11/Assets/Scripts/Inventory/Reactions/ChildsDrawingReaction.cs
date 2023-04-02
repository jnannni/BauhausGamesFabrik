using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class ChildsDrawingReaction : MonoBehaviour
{
    public SignalSend childsDrawingSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;
    [SerializeField] private BoolValue isNearBlob;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "Blocks" && isNearBlob.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();                        
            dialogueRunner.StartDialogue(startNode);
            childsDrawingSignal.Raise();
        }
    }
}
