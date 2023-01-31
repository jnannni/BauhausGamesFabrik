using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    public GameObject inventoryContent;
    public GameObject inventorySelectedButton, pausedMenuSelectedButton, optionsSelectedButton;
    private bool isPaused;
    private bool isInventoryOpen;
    public GameObject pausePanel;
    public string mainMenu;
    public AudioMixer audioMixer;    

    // Start is called before the first frame update
    void Start()
    {
        inventorySelectedButton = inventoryContent.transform.GetChild(0).gameObject;
        Debug.Log(inventorySelectedButton);
        isPaused = false;
        isInventoryOpen = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.I))
        {
            InteractInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
        }

    }

    public void InteractInventory()
    {        
        isInventoryOpen = !isInventoryOpen;
        Debug.Log(inventorySelectedButton);
        if (isInventoryOpen)
        {            
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(inventorySelectedButton);
        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Save()
    {

    }

    public void BackToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
