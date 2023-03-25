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
    public GameObject pausedMenuSelectedButton, optionsSelectedButton;
    public BoolValue isPaused;
    public BoolValue isInventoryOpen;    
    public GameObject pausePanel;
    public string mainMenu;
    public AudioMixer audioMixer;
    private GameObject inventorySelectedButton;
    private EventSystem currentEvenSystem;

    // Start is called before the first frame update
    void Start()
    {             
        isPaused.initialValue = false;
        inventorySelectedButton = inventoryContent.transform.GetChild(0).gameObject;               
        isInventoryOpen.initialValue = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        inventorySelectedButton = inventoryContent.transform.GetChild(0).gameObject;
        if (isPaused.initialValue)
        {
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = 1f;
        }

        if (inventoryPanel.activeSelf && EventSystem.current.currentSelectedGameObject == null)
        {
            inventorySelectedButton.GetComponent<Button>().Select();
        }

        if (Input.GetKeyDown(KeyCode.I) && !pausePanel.activeSelf ||
            Input.GetKeyDown(KeyCode.Escape) && inventoryPanel.activeSelf && !pausePanel.activeSelf)
        {
            inventorySelectedButton = inventoryContent.transform.GetChild(0).gameObject;
            Debug.Log("here");
            InteractInventory();
        } else if (Input.GetKeyDown(KeyCode.Escape) && !inventoryPanel.activeSelf)
        {            
            ChangePause();
        }

    }

    public void InteractInventory()
    {        
        isInventoryOpen.initialValue = !isInventoryOpen.initialValue;
        isPaused.initialValue = !isPaused.initialValue;        
        if (isInventoryOpen.initialValue)
        {            
            EventSystem.current.SetSelectedGameObject(null);            
            inventoryPanel.SetActive(true);            
            EventSystem.current.SetSelectedGameObject(inventorySelectedButton);            
            Debug.Log(EventSystem.current.currentSelectedGameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            inventoryPanel.SetActive(false);            
        }
    }

    public void ChangePause()
    {
        isPaused.initialValue = !isPaused.initialValue;
        if (isPaused.initialValue)
        {
            EventSystem.current.SetSelectedGameObject(null);
            pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(pausedMenuSelectedButton);            
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            pausePanel.SetActive(false);            
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
