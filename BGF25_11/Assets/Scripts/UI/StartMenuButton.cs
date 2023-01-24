using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private GameObject selectionUISign;

    // Start is called before the first frame update
    void Start()
    {
        selectionUISign = transform.Find("Selection").gameObject;
        selectionUISign.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " selected");
        selectionUISign.SetActive(true);        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " deselected");
        selectionUISign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
