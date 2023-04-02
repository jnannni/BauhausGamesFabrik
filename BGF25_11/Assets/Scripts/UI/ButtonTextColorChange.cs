using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextColorChange : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(1f, 0.5529412f, 0.3529412f, 1);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
    }
}
