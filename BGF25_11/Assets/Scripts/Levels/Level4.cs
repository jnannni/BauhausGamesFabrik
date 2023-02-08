using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    public Animator scaredCatAnimator;

    private bool putintheinventory002;
    private bool enterthechurch;
    private bool trigger_TheChurch;
    private bool scurryawayanimation;
    private bool searchforanitem;

    [SerializeField] private string nameOfTheScene;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        dialogueRunner.StartDialogue("TheGraveYard");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trigger_TheChurch", out trigger_TheChurch);
        variableStorage.TryGetValue("$scurryawayanimation", out scurryawayanimation);
        variableStorage.TryGetValue("$scurryawayanimation", out scurryawayanimation);
        variableStorage.TryGetValue("$searchforanitem", out searchforanitem);
        variableStorage.TryGetValue("$putintheinventory002", out putintheinventory002);

        if (scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", true);
        } else if (!scurryawayanimation)
        {
            scaredCatAnimator.SetBool("isScared", false);
        }

        if (trigger_TheChurch)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (enterthechurch)
        {
            // move the character to the church position
        }

        if (putintheinventory002)
        {
            // put stuff in the inventory
        }
    }
}
