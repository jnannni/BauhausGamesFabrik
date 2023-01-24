using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherReaction : MonoBehaviour
{
    public SignalSender featherSignal;

    public void Use(string tag)
    {        
        GameObject player = GameObject.FindWithTag(tag);
        bool currentBool = player.GetComponent<Animator>().GetBool("flying");
        player.GetComponent<Animator>().SetBool("flying", !currentBool);
        featherSignal.Raise();
    }
}
