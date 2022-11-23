using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;//sahneyi kontrol etmek i�in namespacesini ekledik
using UnityEngine.UI;
using yavuz;//bellek y�netimi s�n�f�n� kullan�caz o y�zden import ettik
public class AnaMenu_Manager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();//�rnekledik
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYontem _ReklamYontem = new ReklamYontem();
    public GameObject CikisPaneli;
    public List<ItemBilgileri> _Varsayilan_ItemBilgileri = new List<ItemBilgileri>();
    public List<DilVerileriAnaObje> _Varsayilan_DilVerileri = new List<DilVerileriAnaObje>();
    public AudioSource ButonSes;

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();//s�n�f listesi olu�turduk
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;
    void Start()
    {
        _BellekYonetim.KontrolEtveTanimla();
        _VeriYonetimi.ilkKurulumDosyaOlusturma(_Varsayilan_ItemBilgileri, _Varsayilan_DilVerileri);//di�er t�m itemler bitince aktifle�tir.
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
       _BellekYonetim.VeriKaydet_string("Dil","TR");

        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[0]);
        DilTercihiYonetimi();
    }
    void DilTercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }
    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }
    public void Oyna()
    {
        //SceneManager.LoadScene(4);
        ButonSes.Play();
        StartCoroutine(LoadAsync(_BellekYonetim.VeriOku_i("SonLevel")));
    }
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);//sahne y�kleme oran�n� anl�k veriyor
        YuklemeEkrani.SetActive(true);
        while (!operation.isDone)//sahne y�klemesi tamamlanmad�ysa
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            YuklemeSlider.value = progress;
            yield return null;
        }
    }
    public void CikisButonislem(string Durum)
    {
        ButonSes.Play();
        if (Durum == "Evet")
        {
            Application.Quit();
        }
        else if (Durum == "Cikis")
        {
            CikisPaneli.SetActive(true);
        }
        else
        {
            CikisPaneli.SetActive(false);
        }
    }
}
