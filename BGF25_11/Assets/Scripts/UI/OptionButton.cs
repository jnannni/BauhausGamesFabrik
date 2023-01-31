using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private GameObject selectionUISign;
    private Slider slider;

    
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponent<Slider>();
        
        selectionUISign = transform.Find("Selection").gameObject;
        selectionUISign.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {        
        selectionUISign.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " deselected");
        selectionUISign.SetActive(false);
    }

    public void IncreaseSliderValue()
    {
        slider.value += 1;       
    }

    public void DecreaseSliderValue()
    {
        slider.value += 1;
    }

    public void OnClick()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Keyboard.current.spaceKey.wasPressedThisFrame);
        //Debug.Log(Input.GetKeyDown(KeyCode.D));
        if (Input.GetKeyDown(KeyCode.D) && slider)
        {
            IncreaseSliderValue();
            Debug.Log(slider.value);
        }
        if (Input.GetKeyDown(KeyCode.A) && slider)
        {
            DecreaseSliderValue();
            Debug.Log(slider.value);
        }
    }
}
