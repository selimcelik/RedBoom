using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class testere : MonoBehaviour
{

    GameObject []gidilecekNoktalar;
    bool aradakimesafeyibirkereal = true;
    bool ilerimigerimi = true;
    Vector3 aradakimesafe;
    int aradakimesafesayaci;

    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
        
    }

    
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5);
        noktalaraGit();
    }

    void noktalaraGit()
    {
        if (aradakimesafeyibirkereal)
        {
            aradakimesafe = (gidilecekNoktalar[aradakimesafesayaci].transform.position - transform.position).normalized;
            aradakimesafeyibirkereal = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakimesafesayaci].transform.position);
        transform.position += aradakimesafe * Time.deltaTime*10;
        if (mesafe < 0.5f)
        {
            aradakimesafeyibirkereal = true;
            if(aradakimesafesayaci == gidilecekNoktalar.Length - 1)
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
        
    }
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]
    
class testereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        testere script = (testere)target;

        if (GUILayout.Button("ÜRET")){

            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();

        }
    }
}
#endif