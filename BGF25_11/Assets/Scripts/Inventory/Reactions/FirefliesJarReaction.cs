using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class FirefliesJarReaction : MonoBehaviour
{
    public SignalSend firefliesJarSignal;
    private DialogueRunner dialogueRunner;
    public string startNode;    

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "FrozzenVillage")
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.StartDialogue(startNode);
            firefliesJarSignal.Raise();
        }
    }
}
