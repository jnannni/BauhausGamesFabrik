using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Coin2Reaction : MonoBehaviour
{
    public SignalSend coin2Signal;
    private DialogueRunner dialogueRunner;
    public string startNode;    
    [SerializeField] private BoolValue isNearFontain;    

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "Blocks" && isNearFontain.initialValue)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            coin2Signal.Raise();
        }
    }
}
