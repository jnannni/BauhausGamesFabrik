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
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private BoolValue isInventoryOpen;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "Blocks")
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            coin2Signal.Raise();
        }
    }
}
