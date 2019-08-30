using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class karakterKontrol : MonoBehaviour
{
    public Sprite[] beklemeanim;
    public Sprite[] ziplamaanim;
    public Sprite[] yurumeanim;

    SpriteRenderer spriteRenderer;

    float horizontal = 0;
    float beklemeanimzaman = 0;
    float yurumeanimzaman = 0;
    float siyahArkaPlanSayaci = 0;
    float anaMenuyeDonZaman = 0;

    Rigidbody2D fizik;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    bool birkerezipla = true;

    int beklemeanimsayac = 0;
    int yurumeanimsayac = 0;
    int can = 100;
    int coin = 0;

    GameObject Kamera;

    public Text cantext;
    public Image siyahArkaPlan;
    public Text cointext;

    

    void Start()
    {
        Time.timeScale = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        siyahArkaPlan.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }
        
        Kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPos = Kamera.transform.position - transform.position;

        cantext.text = "CAN = " + can;
        cointext.text = "COİN = " + coin;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (birkerezipla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birkerezipla = false;
            }
        }
        
    }

    void LateUpdate()
    {
        kameraKontrol();
    }

    void FixedUpdate()
    {
        karakterhareket();
        Animasyon();
        if (can <= 0)
        {
            Time.timeScale = 0.5f;
            cantext.enabled = false;
            siyahArkaPlan.gameObject.SetActive(true);
            siyahArkaPlanSayaci += 0.03f;
            siyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman > 1)
            {
                SceneManager.LoadScene("AnaMenu");
            }
        }
    }
    void karakterhareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        birkerezipla = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "kursun")
        {
            can-=5;
            cantext.text = "CAN = " + can;
        }
        if (coll.gameObject.tag == "dusman")
        {
            can -= 20;
            cantext.text = "CAN = " + can;
        }
        if (coll.gameObject.tag == "testere")
        {
            can -= 10;
            cantext.text = "CAN = " + can;
        }
        if (coll.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if (coll.gameObject.tag == "canver")
        {
            can += 30;
            if (can >= 100)
            {
                can = 100;
                cantext.text = "CAN = " + can;
            }
            cantext.text = "CAN = " + can;
            coll.GetComponent<BoxCollider2D>().enabled = false;
            coll.GetComponent<canver>().enabled = true;
            Destroy(coll.gameObject, 3);
        }
        if (coll.gameObject.tag == "coin")
        {
            coin += 1;
            cointext.text = "COIN = " + coin;
            coll.GetComponent<CircleCollider2D>().enabled = false;
            coll.GetComponent<cointopla>().enabled = true;
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "su")
        {
            can = 0;
        }
        if (coll.gameObject.tag == "sinir")
        {
            can = 0;
        }

    }

    void kameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        Kamera.transform.position = Vector3.Lerp(Kamera.transform.position, kameraSonPos, 0.1f);
    }

    void Animasyon()
    {
        if (birkerezipla)
        {
            if (horizontal == 0)
            {
                beklemeanimzaman += Time.deltaTime;
                if (beklemeanimzaman > 0.05f)
                {


                    spriteRenderer.sprite = beklemeanim[beklemeanimsayac++];
                    if (beklemeanimsayac == beklemeanim.Length)
                    {
                        beklemeanimsayac = 0;
                    }
                    beklemeanimzaman = 0;
                }
            }
            else if (horizontal > 0)
            {
                yurumeanimzaman += Time.deltaTime;
                if (yurumeanimzaman > 0.01f)
                {


                    spriteRenderer.sprite = yurumeanim[yurumeanimsayac++];
                    if (yurumeanimsayac == yurumeanim.Length)
                    {
                        yurumeanimsayac = 0;
                    }
                    yurumeanimzaman = 0;
                    
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                yurumeanimzaman += Time.deltaTime;
                if (yurumeanimzaman > 0.01f)
                {


                    spriteRenderer.sprite = yurumeanim[yurumeanimsayac++];
                    if (yurumeanimsayac == yurumeanim.Length)
                    {
                        yurumeanimsayac = 0;
                    }
                    yurumeanimzaman = 0;

                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (fizik.velocity.y > 0)
            {
                spriteRenderer.sprite = ziplamaanim[0];
            }
            else
            {
                spriteRenderer.sprite = ziplamaanim[1];
            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
        
       
    }
