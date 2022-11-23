using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using yavuz;

public class Level_Manager : MonoBehaviour
{
    public Button[] Butonlar;
    public int Level;
    public Sprite KilitButon;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    public AudioSource ButonSes;

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();//sýnýf listesi oluþturduk
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();

    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;
    void Start()
    {
        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[2]);
        DilTercihiYonetimi();
        //ButonSes.volume = PlayerPrefs.GetFloat("MenuFx"); bellek yönetimi olduðu için ordan okuduk aþaðýda
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
        //_BellekYonetim.VeriKaydet_int("SonLevel", Level);
        int mevcutLevel = _BellekYonetim.VeriOku_i("SonLevel") - 4;
        int Index = 1;
        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (i + 1 <= mevcutLevel)
            {
                Butonlar[i].GetComponentInChildren<TextMeshProUGUI>().text = Index.ToString();
                int SahneIndex = Index + 4;
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(SahneIndex); });
            }
            else
            {
                Butonlar[i].GetComponent<Image>().sprite = KilitButon;
                //Butonlar[i].interactable = false;//týklanabilirliði kapatýlýyor butonun
                Butonlar[i].enabled = false;//yukardakine göre silik bir imaj býrakmadan direk butonun pasif ediyor
            }
            Index++;
        }

    }
    void DilTercihiYonetimi()
    {
        //if (_BellekYonetim.VeriOku_s("Dil") == "TR")
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
        StartCoroutine(LoadAsync(Index));
    }
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);//sahne yükleme oranýný anlýk veriyor
        YuklemeEkrani.SetActive(true);
        while (!operation.isDone)//sahne yüklemesi tamamlanmadýysa
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            YuklemeSlider.value = progress;
            yield return null;
        }
    }
    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }
}
