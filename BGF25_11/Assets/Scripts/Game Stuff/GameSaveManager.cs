using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public GameObject dialogueRinner;

    private void Awake()
    {
        if (gameSave == null)
        {
            gameSave = this;
        } else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
