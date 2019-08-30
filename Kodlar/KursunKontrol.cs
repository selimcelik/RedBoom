using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KursunKontrol : MonoBehaviour
{
    Rigidbody2D fizik;
    DusmanKontrol dusman;
    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<DusmanKontrol>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon()*1000);

    }

    
    void Update()
    {
        
    }
}
