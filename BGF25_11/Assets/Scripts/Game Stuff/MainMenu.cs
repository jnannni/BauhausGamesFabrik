using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool gameExists;
    private bool startTheNewGame;
    private bool startLevels;
    private FadeLayer fadeLayer;
    public EventSystem eventSystem;    
    public GameObject optionsMenu, mainMenu;
    public Button optionsButton;
    public GameObject optionsFirstButton, optionsClosedFirstButton;
    [SerializeField] private Animator transitionAnimator;
    private AudioManager audioManager;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance02;
    private bool instanceallowed;
    private bool instanceallowed02;

    private void Awake()
    {
        //audioManager = FindObjectOfType<AudioManager>();
        fadeLayer = FindObjectOfType<FadeLayer>();
        Time.timeScale = 1f;
    }

    private void Start()
    {
        //audioManager.InitializeMusic(FMODEvents.instance.mainMenuMusic);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TitleScreenMenu");
        instance.start();
        startTheNewGame = false;   
        instanceallowed = true;     
        gameExists = false;
        startLevels = false;
        if (!gameExists)
        {
            //loadGameButton.enabled = false;
            optionsButton.enabled = false;
            //loadGameButton.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5566038f, 0.5566038f, 0.5566038f, 0.6235294f);
            optionsButton.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5566038f, 0.5566038f, 0.5566038f, 0.6235294f);
        }
    }

    private void Update()
    {
        if (startTheNewGame)
        {
            transitionAnimator.SetBool("transitiontoww", true);
            if (instanceallowed)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                instance02 = FMODUnity.RuntimeManager.CreateInstance("event:/UI Sounds/Transition_WW_to_DW");
                instance02.start();
                instanceallowed = false;
            }
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                startTheNewGame = false;
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene("SampleScene");
                transitionAnimator.SetBool("transitiontoww", false);
                instance02.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance02.release();
            }
        }

        if (startLevels)
        {
            transitionAnimator.SetBool("transitiontoww", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                startLevels = false;
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene("End");
                transitionAnimator.SetBool("transitiontoww", false);
            }
        }
    }

    public void NewGame()
    {
        startTheNewGame = true;
              
    }

    public void LevelsScreen()
    {
        startLevels = true;  
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void LoadGame()
    {

    }

    public void OnClickOption()
    {        
        if (!mainMenu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
            optionsFirstButton.GetComponent<Button>().Select();
        } else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsClosedFirstButton);
            optionsClosedFirstButton.GetComponent<Button>().Select();
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }  
}
