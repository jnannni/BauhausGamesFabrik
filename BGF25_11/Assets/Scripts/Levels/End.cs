using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject levels;

    [SerializeField] private Button firstSelected;    

    // Start is called before the first frame update
    void Start()
    {
        //firstSelected = levels.GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    // Update is called once per frame
    void Update()
    {        
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            firstSelected.Select();
        }
    }

    public void OnButtonSelected(string sceneName)
    {
        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
