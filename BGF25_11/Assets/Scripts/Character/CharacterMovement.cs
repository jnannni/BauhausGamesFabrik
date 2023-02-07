using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class CharacterMovement : MonoBehaviour
{
    public enum PlayerState
    {
        none,
        walk,
        interact,
        fly,
        cut_scene
    }

    private PlayerState currentState = PlayerState.none;
    public Rigidbody2D rb;
    public BoolValue isPaused;
    private Vector2 movement;
    public float speed = 5f;
    public Animator animator;
    [SerializeField] private BoolValue isDialogueRunning;

    // audio
    private EventInstance playerFootsteps;

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
    }

    // Update is called once per frame
    void Update()
    {        
        animator.SetBool("isDialogueRunning", isDialogueRunning.initialValue);
        movement = Vector2.zero;
        if (!isPaused.initialValue)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }        

        if (!isDialogueRunning.initialValue && movement != Vector2.zero)
        {
            currentState = PlayerState.walk;
            Movement();
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("walking", true);
        }
        else
        {
            currentState = PlayerState.none;
            animator.SetBool("walking", false);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
        UpdateSound();
    }

    private void Movement()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);        
    }

    private void UpdateSound()
    {
        if (currentState == PlayerState.walk)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        } else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
