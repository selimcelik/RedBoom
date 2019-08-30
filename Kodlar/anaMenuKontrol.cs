using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenuKontrol : MonoBehaviour
{
    GameObject leveller;
    GameObject kilitler;

    void Start()
    {

        leveller = GameObject.Find("leveller");
        kilitler = GameObject.Find("kilitler");

        PlayerPrefs.DeleteAll();
        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < kilitler.transform.childCount; i++)
        {
            kilitler.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }

    }
    public void butonSec(int gelenbuton)
    {
        if (gelenbuton == 1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("kacincilevel"));
        }
        else if (gelenbuton == 2)
        {
            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < kilitler.transform.childCount; i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(false); 
            }

        }
        else if (gelenbuton== 3)
        {
            Application.Quit();
        }
    }
    public void levellerButon(int gelenlevel)
    {
        SceneManager.LoadScene(gelenlevel);
    }
}
