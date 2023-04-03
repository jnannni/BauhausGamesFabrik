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
    private FadeLayer fadeLayer;
    public EventSystem eventSystem;    
    public GameObject optionsMenu, mainMenu;
    public Button loadGameButton, optionsButton;
    public GameObject optionsFirstButton, optionsClosedFirstButton;
    [SerializeField] private Animator transitionAnimator;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        fadeLayer = FindObjectOfType<FadeLayer>();
    }

    private void Start()
    {
        audioManager.InitializeMusic(FMODEvents.instance.mainMenuMusic);
        startTheNewGame = false;        
        gameExists = false;
        if (!gameExists)
        {
            loadGameButton.enabled = false;
            optionsButton.enabled = false;
            loadGameButton.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5566038f, 0.5566038f, 0.5566038f, 0.6235294f);
            optionsButton.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5566038f, 0.5566038f, 0.5566038f, 0.6235294f);
        }
    }

    private void Update()
    {
        if (startTheNewGame)
        {
            transitionAnimator.SetBool("transitiontoww", true);
            if (transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transitiontoww"))
            {
                startTheNewGame = false;
                StartCoroutine(fadeLayer.FadeIn());
                SceneManager.LoadScene("SampleScene");
                transitionAnimator.SetBool("transitiontoww", false);
            }
        }
    }

    public void NewGame()
    {
        startTheNewGame = true;
              
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
