using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class DivingMaskReaction : MonoBehaviour
{
    public SignalSend divingMaskSignal;    
    [SerializeField] private BoolValue isNearPond;
    [SerializeField] private BoolValue isMaskUsed;

    public void Use()
    {
        if (SceneManager.GetActiveScene().name == "DreamBlocks" && isNearPond.initialValue)
        {
            isMaskUsed.initialValue = true;
            divingMaskSignal.Raise();
        }
    }
}
