using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool gameExists;       
    public EventSystem eventSystem;    
    public GameObject optionsMenu, mainMenu;
    public Button loadGameButton;
    public GameObject optionsFirstButton, optionsClosedFirstButton;

    private void Start()
    {
        gameExists = false;
        if (!gameExists)
        {
            loadGameButton.enabled = false;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
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

    private void Update()
    {
        
        
    }
}
