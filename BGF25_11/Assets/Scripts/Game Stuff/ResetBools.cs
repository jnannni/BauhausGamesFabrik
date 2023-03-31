using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBools : MonoBehaviour
{
    public BoolValue[] boolValues;

    private void Start()
    {
        foreach (BoolValue boolValue in boolValues)
        {
            boolValue.initialValue = false;
        }
    }
}
