using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Material material;
    private bool isDisolving;
    private float fade = 1f;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;        
    }

    private void Update()
    {
        if (fade == 1f || isDisolving)
        {

        }
    }

    public bool isDissolved()
    {
        return isDisolving;
    }

    public void Dissolving()
    {        
        while (fade <= 0f)
        {
            Debug.Log(fade);
            isDisolving = true;
            fade -= Time.deltaTime;
            material.SetFloat("_Fade", fade);
        }
        if (fade <= 0)
        {
            Debug.Log("<0 " + fade);
            isDisolving = false;
        }        
    }
}
