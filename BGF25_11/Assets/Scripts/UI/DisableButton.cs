using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().enabled = false;
        this.gameObject.GetComponentInChildren<TMP_Text>().color = new Color(0.5566038f, 0.5566038f, 0.5566038f, 0.6235294f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
