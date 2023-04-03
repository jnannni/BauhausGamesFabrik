using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManagerDemo : MonoBehaviour
{

    public GameObject pausePanel;
    private Button firstSelected;
    // Start is called before the first frame update

    private void Awake()
    {
        firstSelected = pausePanel.GetComponent<Button>();
    }

    void Start()
    {
        firstSelected.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
