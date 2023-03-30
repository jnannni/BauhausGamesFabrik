using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System;

public class CustomDialogueView : DialogueViewBase
{
    [SerializeField] Animator animator;
    [SerializeField] DialogueRunner runner;
    [Header("Assets"), Tooltip("you can manually assign various assets here if you don't want to use /Resources/ folder")]
    public List<Sprite> loadSprites = new List<Sprite>();
    private string previousSprite = "";

    // big lists to keep track of all instantiated objects    
    List<Image> sprites = new List<Image>(); // big list of all instantianted sprites

    [SerializeField] Image portraitPosition;

    private void Awake()
    {
        runner.AddCommandHandler<string>("SetPortrait", SetPortrait);        
    }
   
    public void SetPortrait(string spriteName)
    {       
        if (previousSprite != spriteName)
        {
            foreach (var param in animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Bool)
                {                    
                    animator.SetBool(previousSprite, false);
                }
            }
        }               
        animator.SetBool(spriteName, true);    
        portraitPosition.sprite = loadSprites.Find(sprite => sprite.name == spriteName);
        previousSprite = spriteName;
    }

    public void PlaySound()
    {
        Debug.Log("Sound is playing");
    }   

}
