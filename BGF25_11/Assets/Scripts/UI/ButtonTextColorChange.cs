using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextColorChange : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float red;
    [SerializeField] private float green;
    [SerializeField] private float blue;

    [SerializeField] private float red_d;
    [SerializeField] private float green_d;
    [SerializeField] private float blue_d;

    public void OnSelect(BaseEventData eventData)
    {
        //this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(1f, 0.5529412f, 0.3529412f, 1);
        this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(red, green, blue, 1);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
        this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(red_d, green_d, blue_d, 1);
    }
}
