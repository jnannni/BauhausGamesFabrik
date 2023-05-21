using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AutomaticLight : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    [SerializeField] private bool oneUse;

    public Animator lightAnimator;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lightAnimator.SetBool("darkdown", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        lightAnimator.SetBool("darkdown", false);
    }
}
