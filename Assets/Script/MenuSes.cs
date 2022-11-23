using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance;//objenin daha önce oluituruþup oluþturulmadýðý kontrol edilecek
    public AudioSource Ses;
    void Start()
    {
        Ses.volume = PlayerPrefs.GetFloat("MenuSes"); 
        DontDestroyOnLoad(gameObject);//sahneler arasý geçiþte bu objeyi kaybetmeyeceksin
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
        Ses.volume = PlayerPrefs.GetFloat("MenuSes");//artýk anlýk olarak menu sesini alýcak
    }
}
