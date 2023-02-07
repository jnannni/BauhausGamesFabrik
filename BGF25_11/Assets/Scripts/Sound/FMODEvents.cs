using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Interactable NPCs")]
    [field: SerializeField] public List<EventReference> npsAura { get; private set; }

    [field: Header("Collected Item")]
    [field: SerializeField] public EventReference collectedItem { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events script.");
        }
        instance = this;
    }

    private void Start()
    {
        //npsAura = new List<EventReference>();
    }
}
