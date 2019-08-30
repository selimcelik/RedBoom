using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class DusmanKontrol : MonoBehaviour
{

    GameObject[] gidilecekNoktalar;
    GameObject karakter;

    bool aradakimesafeyibirkereal = true;
    bool ilerimigerimi = true;

    int aradakimesafesayaci;
    int hiz;

    Vector3 aradakimesafe;

    RaycastHit2D ray;

    public LayerMask layermask;
 

    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriteRenderer;

    public GameObject kursun;

    float ateszamani = 0;

    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        karakter = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }

    }


    void FixedUpdate()
    {
        
        benigordumu();
        if (ray.collider.tag=="Player")
        {
            hiz = 8;
            spriteRenderer.sprite = onTaraf;
            atesEt();
        }
        else
        {
            hiz = 4;
            spriteRenderer.sprite = arkaTaraf;
        }

        noktalaraGit();
    }
    void atesEt()
    {
        ateszamani += Time.deltaTime;
        if (ateszamani > Random.Range(0.2f, 1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            ateszamani = 0;

        }
    }
    void benigordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000,layermask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }

    void noktalaraGit()
    {
        if (aradakimesafeyibirkereal)
        {
            aradakimesafe = (gidilecekNoktalar[aradakimesafesayaci].transform.position - transform.position).normalized;
            aradakimesafeyibirkereal = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakimesafesayaci].transform.position);
        transform.position += aradakimesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakimesafeyibirkereal = true;
            if (aradakimesafesayaci == gidilecekNoktalar.Length - 1)
            {
                ilerimigerimi = false;
            }
            else if (aradakimesafesayaci == 0)
            {
                ilerimigerimi = true;
            }
            if (ilerimigerimi)
            {
                aradakimesafesayaci++;
            }
            else
            {
                aradakimesafesayaci--;
            }
        }
    }
    public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }

    }
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(DusmanKontrol))]
[System.Serializable]

class DusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DusmanKontrol script = (DusmanKontrol)target;

        if (GUILayout.Button("ÜRET"))
        {

            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();

        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif