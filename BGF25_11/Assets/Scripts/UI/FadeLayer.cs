using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeLayer : MonoBehaviour
{
    // Whether we start the scene fully faded out (i.e. 100% black) or not
    [SerializeField] bool startFadedOut = false;
    private CanvasGroup canvasGroup; // canvas UI layer we are fading

    // when this fade layer object is created
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = startFadedOut ? 1 : 0;
    }

    // A coroutine that fades to transparency {alpha} over {time} seconds
    public IEnumerator ChangeAlphaOverTime(float alpha, float time)
    {
        float currentAlpha = canvasGroup.alpha;
        float elapsed = 0f;
        while (elapsed < time)
        {
            var factor = elapsed / time;
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, alpha, factor);
            yield return null;
            elapsed += Time.deltaTime;
        }
        canvasGroup.alpha = alpha;
    }

    public IEnumerator FadeIn()
    {
        for (float alpha = 1f; alpha >= 0; alpha -=0.1f)
        {
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator FadeOut()
    {
        for (float alpha = 0; alpha > 1f; alpha += 0.1f)
        {
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(.1f);
        }
    }
}