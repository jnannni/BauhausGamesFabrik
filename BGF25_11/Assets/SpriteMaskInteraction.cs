using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskInteraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteMain;
    [SerializeField] private SpriteMask spriteMask;

    // Update is called once per frame
    void Update()
    {
        if (spriteMask.sprite != spriteMain.sprite)
        {
            spriteMask.sprite = spriteMain.sprite;
        }
        
    }
}
