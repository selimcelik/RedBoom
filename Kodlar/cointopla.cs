using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cointopla : MonoBehaviour
{
    public Sprite[] animasyonkareleri;
    SpriteRenderer spriteRenderer;
    int animasyonkarelerisayac = 0;
    float zaman = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.03f)
        {
            spriteRenderer.sprite = animasyonkareleri[animasyonkarelerisayac++];
            if (animasyonkareleri.Length == animasyonkarelerisayac)
            {
                animasyonkarelerisayac = 0;
            }
            zaman = 0;
        }
        
    }
}
