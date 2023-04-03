using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference musicWW1 { get; private set; }
    [field: SerializeField] public EventReference musicWW2 { get; private set; }
    [field: SerializeField] public EventReference musicGraveyard { get; private set; }
    [field: SerializeField] public EventReference musicBlocks { get; private set; }
    [field: SerializeField] public EventReference musicTheG { get; private set; }
    [field: SerializeField] public EventReference musicMuseumCorner { get; private set; }
    [field: SerializeField] public EventReference musicTrainStation { get; private set; }
    [field: SerializeField] public EventReference musicWorkerQuarters { get; private set; }
    [field: SerializeField] public EventReference musicFabric { get; private set; }
    [field: SerializeField] public EventReference musicBeach { get; private set; }
    [field: SerializeField] public EventReference musicAmusementPark { get; private set; }
    [field: SerializeField] public EventReference musicDW1 { get; private set; }
    [field: SerializeField] public EventReference musicGravedream { get; private set; }
    [field: SerializeField] public EventReference musicDreamBlocks { get; private set; }
    [field: SerializeField] public EventReference musicFrozenVillage { get; private set; }
    [field: SerializeField] public EventReference musicRedBeach { get; private set; }
    [field: SerializeField] public EventReference musicChurch { get; private set; }
    [field: SerializeField] public EventReference golemCutScene { get; private set; }
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Interactable NPCs")]
    [field: SerializeField] public List<EventReference> npsAura { get; private set; }

    [field: Header("Collected Item")]
    [field: SerializeField] public EventReference collectedItem { get; private set; }

    [field: Header("UI Sounds")]
    [field: SerializeField] public EventReference iniventoryClose { get; private set; }
    [field: SerializeField] public EventReference inventoryOpen { get; private set; }
    [field: SerializeField] public EventReference transitionToWW { get; private set; }
    [field: SerializeField] public EventReference transitionToDW { get; private set; }
    [field: SerializeField] public EventReference transitionBetweenWW { get; private set; }
    [field: SerializeField] public EventReference dialogueClick { get; private set; }

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
