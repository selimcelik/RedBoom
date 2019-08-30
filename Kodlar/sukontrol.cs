using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sukontrol : MonoBehaviour
{
    public Sprite[] suAnimasyon;
    SpriteRenderer spriteRenderer;
    int animasyonkarelerisayac;
    float zaman = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.04f)
        {
            spriteRenderer.sprite = suAnimasyon[animasyonkarelerisayac++];
            if (suAnimasyon.Length == animasyonkarelerisayac)
            {
                animasyonkarelerisayac = 0;
            }
            zaman = 0;
        }
    }
}
