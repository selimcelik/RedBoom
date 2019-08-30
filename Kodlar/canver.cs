using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canver : MonoBehaviour
{
    public Sprite []animasyonkareleri;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonkareleriSayaci = 0;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        zaman += Time.deltaTime;
        
        if (zaman > 0.5f)
        {
            spriteRenderer.sprite = animasyonkareleri[animasyonkareleriSayaci++];
            if (animasyonkareleri.Length == animasyonkareleriSayaci)
            {
                animasyonkareleriSayaci = animasyonkareleri.Length-1;
            }
            zaman = 0;
        }
    }
}
