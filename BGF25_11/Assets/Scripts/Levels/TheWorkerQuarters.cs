using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TheWorkerQuarters : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;

    private bool loopthemap;
    private bool theworkercomesout;
    private bool cutscene_rabbits;
    private bool loopthetext;
    private bool trigger_Frozen;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private BoolValue isLoopTriggered;
    [SerializeField] private BoolValue changeCameraSmoothing;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private string nameOfTheScene;

    private Vector3 playerStartPosition;
    private Vector3 playerCurrentPosition;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("TheWorkerQuarters");
        playerCurrentPosition = player.transform.position;
        playerStartPosition = playerPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentPosition = player.transform.position;
        playerStartPosition = new Vector3(playerPosition.transform.position.x, playerCurrentPosition.y, 0);

        if (isLoopTriggered.initialValue)
        {
            player.transform.position = playerStartPosition;            
            cam.smoothing = 1f;
            isLoopTriggered.initialValue = false;
        }

        if (changeCameraSmoothing.initialValue)
        {
            cam.smoothing = 0.1f;
            changeCameraSmoothing.initialValue = false;
        }
    }    
}
