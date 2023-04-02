using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class StrangeBookReaction : MonoBehaviour
{
    public SignalSend griefingBookSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;
    [SerializeField] private BoolValue isNearSecurity;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "TheG" && isNearSecurity.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            griefingBookSignal.Raise();
        }
    }
}
