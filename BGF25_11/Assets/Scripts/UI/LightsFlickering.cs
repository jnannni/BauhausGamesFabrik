using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsFlickering : MonoBehaviour
{

    Transform mainLight;
    Transform flickerLight;
    Light2D mainLightComponent;
    Light2D flickerLightComponent;
    public float minTime, maxTime, minIntensity, maxIntensity;


    // Start is called before the first frame update
    void Start()
    {        
        flickerLight = this.transform.GetChild(0);        
        flickerLightComponent = flickerLight.GetComponent<Light2D>();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (; ; ) //this is while(true)
        {
            float randomIntensity = Random.Range(minIntensity, maxIntensity); // 1.5 3.5
            flickerLightComponent.intensity = randomIntensity;


            float randomTime = Random.Range(minTime, maxTime); // 0 0.1
            yield return new WaitForSeconds(randomTime);
        }
    }
}