using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TrainStation : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    [SerializeField] private GameObject player;
    [SerializeField] private Sprite mapImage;

    private bool trainapproaching;
    private bool trigger_TheFactory;
    private bool lookatthemap;

    [SerializeField] private string nameOfTheScene;    
    [SerializeField] private BoolValue isIllustrationWatched;
    [SerializeField] private GameObject illustrationPanel;

    private FadeLayer fadeLayer;

    private void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("BackInTheWaking");
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$trainapproaching", out trainapproaching);
        variableStorage.TryGetValue("$trigger_TheFactory", out trigger_TheFactory);
        variableStorage.TryGetValue("$lookatthemap", out lookatthemap);

        if (trigger_TheFactory)
        {
            SceneManager.LoadScene(nameOfTheScene);
        }

        if (lookatthemap && !isIllustrationWatched.initialValue)
        {
            isIllustrationWatched.initialValue = true;
            illustrationPanel.transform.GetChild(0).GetComponent<Image>().sprite = mapImage;
            variableStorage.SetValue("$lookatthemap", false);
        }
    }
}
