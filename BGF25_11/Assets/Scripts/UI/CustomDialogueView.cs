using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CustomDialogueView : DialogueViewBase
{
    // The amount of time that lines will take to appear.
    [SerializeField] private float appearanceTime = 0.5f;

    // The amount of time that lines will take to disappear.
    [SerializeField] private float disappearanceTime = 0.5f;

    // The text view to display the line of dialogue in.
    [SerializeField] TMPro.TextMeshProUGUI text;

    // The game object that should animate in and out.
    [SerializeField] RectTransform container;

    // If this is true, then the line view will not automatically report that
    // it's done showing a line, and will instead wait for InterruptLine to be
    // called (which happens when UserRequestedViewAdvancement is called.)
    [SerializeField] private bool waitForInput;

    // The current coroutine that's playing out a scaling animation. When this
    // is not null, we're in the middle of an animation.
    Coroutine currentAnimation;

   
}
