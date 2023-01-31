using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShadow : MonoBehaviour
{
    public GameObject player;

    public GameObject pixelPrefab;

    List<SpriteRenderer> pixels;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = (RectTransform)player.transform;
        int pixelWidth = (int)rt.rect.width;
        Vector2 startingPoint = Vector2.left * rt.rect.width * 0.5f;

        for  (int i = 0; i < pixelWidth; i++)
        {
            Vector2 offset = startingPoint + (Vector2.right * i * 0.01f);
            pixels.Add(GameObject.Instantiate(pixelPrefab, transform).GetComponent<SpriteRenderer>());
            pixels[i].transform.localPosition = offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
