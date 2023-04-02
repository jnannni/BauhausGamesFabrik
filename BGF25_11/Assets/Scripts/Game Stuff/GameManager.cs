using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private FadeLayer fadeLayer;
    public BoolValue isDialogueRunning;    
    public Animator animator;
    public Animator portraitAnimator;
    private AudioManager audioManager;

    public GameObject continueClue;
    public GameObject textPanel;
    public GameObject lastLine;
    private string previousLine = "";
    private string currentLine;
    // Start is called before the first frame update
    void Awake()
    {
        fadeLayer = FindObjectOfType<FadeLayer>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        isDialogueRunning.initialValue = false;
        dialogueRunner.AddCommandHandler<float>("fadeIn", FadeIn);
        dialogueRunner.AddCommandHandler<float>("fadeOut", FadeOut);
        audioManager = FindObjectOfType<AudioManager>();        
    }

    private void Update()
    {       
        if (isDialogueRunning.initialValue && continueClue.activeSelf && Input.GetKeyDown(KeyCode.M))
        {            
            audioManager.PlayOneShot(FMODEvents.instance.dialogueClick, this.transform.position);            
        }
    }    

    public void DialogueIsRunning()
    {
        animator.SetBool("isDialogueRunning", true);
        isDialogueRunning.initialValue = true;
    }

    public void DialogueIsNotRunning()
    {
        foreach (var param in portraitAnimator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                portraitAnimator.SetBool(param.name, false);
            }
        }
        animator.SetBool("isDialogueRunning", false);
        isDialogueRunning.initialValue = false;
    }

    private Coroutine FadeIn(float time = 1f)
    {
        //return StartCoroutine(fadeLayer.ChangeAlphaOverTime(0, time));
        return StartCoroutine(fadeLayer.FadeIn());
    }

    private Coroutine FadeOut(float time = 1f)
    {
        //return StartCoroutine(fadeLayer.ChangeAlphaOverTime(1, time));
        return StartCoroutine(fadeLayer.FadeOut());
    }
}
