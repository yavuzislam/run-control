using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance;//objenin daha �nce oluituru�up olu�turulmad��� kontrol edilecek
    public AudioSource Ses;
    void Start()
    {
        Ses.volume = PlayerPrefs.GetFloat("MenuSes"); 
        DontDestroyOnLoad(gameObject);//sahneler aras� ge�i�te bu objeyi kaybetmeyeceksin
        if (instance==null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Ses.volume = PlayerPrefs.GetFloat("MenuSes");//art�k anl�k olarak menu sesini al�cak
    }
}
