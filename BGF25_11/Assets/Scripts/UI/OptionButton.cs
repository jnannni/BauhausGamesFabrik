using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private GameObject selectionUISign;
    [SerializeField] private Slider slider;

    private bool dDown, aDown;

    
    // Start is called before the first frame update
    void Start()
    {                
        selectionUISign = transform.Find("Selection").gameObject;
        selectionUISign.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {        
        selectionUISign.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {        
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
   
    void Update()
    {
        CheckIfAKeyPressed();
        CheckIfDKeyPressed();
        /*Debug.Log(aDown + "   " + dDown);
        Debug.Log(Input.GetKeyDown(KeyCode.E) + " " + slider);
        if (dDown && slider)
        {
            IncreaseSliderValue();
            dDown = false;
        }
        if (aDown && slider)
        {
            DecreaseSliderValue();
            aDown = false;
        }*/
    }

    void CheckIfDKeyPressed()
    {
        dDown |= Input.GetKeyDown(KeyCode.D);
        if (dDown && slider)
        {
            IncreaseSliderValue();
            dDown = false;
        }
    }

    void CheckIfAKeyPressed()
    {
        aDown |= Input.GetKeyDown(KeyCode.A);
        if (aDown && slider)
        {
            DecreaseSliderValue();
            aDown = false;
        }
    }
}
