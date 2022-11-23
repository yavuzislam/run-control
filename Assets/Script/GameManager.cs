using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using yavuz;//kendi kütüphanemi cagirdim
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int AnlikKarakterSayisi = 1;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektleri;
    //public GameObject Hedef;
    //public GameObject PrefabKarakter;
    //public GameObject DogmaNoktasi;
    [Header("LEVEL VERÝLERÝ")]//unity arayüzünde level verilerini kapsayan bölge oluþtu.
    public List<GameObject> Dusmanlar;
    public int DusmanSayisi;
    public GameObject _AnaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi;
    [Header("---SAPKALAR")]
    public GameObject[] Sapkalar;
    [Header("---SOPALAR")]
    public GameObject[] Sopalar;
    [Header("---MATERYALLER")]
    public Material[] Materyaller;
    public SkinnedMeshRenderer _Renderer;
    public Material VarsayilanTema;

    Matematiksel_islemler _Matematiksel_islemler = new Matematiksel_islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYontem _ReklamYontem = new ReklamYontem();

    Scene _Scene;
    [Header("-----GENEL VERÝLERÝ")]
    public AudioSource[] Sesler;
    public GameObject[] islemPanelleri;
    public Slider OyunSesiAyar;

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();//sýnýf listesi oluþturduk
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;
    [Header("-----YÜKLEME VERÝLERÝ")]
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;
    private void Awake()
    {
        Sesler[0].volume = _BellekYonetim.VeriOku_f("OyunSes");
        OyunSesiAyar.value = _BellekYonetim.VeriOku_f("OyunSes");
        Sesler[1].volume = _BellekYonetim.VeriOku_f("MenuFx");
        Destroy(GameObject.FindWithTag("MenuSes"));
        ItemleriKontolEt();
    }
    void Start()
    {
        DusmanlariOlustur();
        _Scene = SceneManager.GetActiveScene();//aktif olan sahnenin degerlerini al.
        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[5]);
        DilTercihiYonetimi();

        _ReklamYontem.RequestInterstitial();
        
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

    public void DusmanlariOlustur()
    {
        for (int i = 0; i < DusmanSayisi; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikmi = true;
        SavasDurumu();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    foreach (var item in Karakterler)
        //    {
        //        if (!item.activeInHierarchy)
        //        {
        //            item.transform.position = DogmaNoktasi.transform.position;
        //            item.SetActive(true);
        //            AnlikKarakterSayisi++;
        //            break;
        //        }
        //    }
        //}
    }
    void SavasDurumu()
    {
        if (SonaGeldikmi)
        {
            if (AnlikKarakterSayisi == 1 || DusmanSayisi == 0)
            {
                OyunBittimi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                _ReklamYontem.GecisReklamiGoster();

                if (AnlikKarakterSayisi <= DusmanSayisi)
                {
                    islemPanelleri[3].SetActive(true);
                }
                else
                {
                    if (AnlikKarakterSayisi>4)
                    {
                        if (_Scene.buildIndex== _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 300);
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    else
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 100);
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    islemPanelleri[2].SetActive(true);
                }
            }
        }

    }
    public void AdamYonetim(string islemTuru, int gelenSayi, Transform Pozisyon)
    {
        switch (islemTuru)
        {
            case "Carpma":
                _Matematiksel_islemler.Carpma(gelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;
            case "Toplama":
                _Matematiksel_islemler.Toplama(gelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;
            case "Cikarma":
                _Matematiksel_islemler.Cikarma(gelenSayi, Karakterler, YokOlmaEfektleri);
                break;
            case "Bolme":
                _Matematiksel_islemler.Bolme(gelenSayi, Karakterler, YokOlmaEfektleri);
                break;
        }
    }
    public void YokOlmaEfektiOlustur(Vector3 Pozisyon, bool Balyoz = false, bool Durum = false)
    //adamlekerini burdan balyozun aktif olma durmuna göre kontrol ediyoruz.
    //alt karakter balyozla etkilþime girince çalýþacak fonksiyon aktif olursa yani=true burdaki yok olma efekti çalýþacak.
    //yani biz balyoz için ekstradan adam lekesi fonksiyonu oluþturmacaðýz.
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!Durum)
                {
                    AnlikKarakterSayisi--;
                }
                else
                {
                    DusmanSayisi--;
                }

                break;
            }
        }
        if (Balyoz)
        {
            Vector3 yeniPoz = new Vector3(Pozisyon.x, .005f, Pozisyon.z);
            foreach (var item in AdamLekesiEfektleri)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = yeniPoz;
                    break;
                }
            }
        }
        if (!OyunBittimi)//2 kere çalýþmamasý için böyle yapýldý
        {
            SavasDurumu();
        }
    }
    public void ItemleriKontolEt()
    {
        if (_BellekYonetim.VeriOku_i("AktifSapka")!=-1)
        {
            Sapkalar[_BellekYonetim.VeriOku_i("AktifSapka")].SetActive(true);
        }
        if (_BellekYonetim.VeriOku_i("AktifSopa") != -1)
            Sopalar[_BellekYonetim.VeriOku_i("AktifSopa")].SetActive(true);
        if (_BellekYonetim.VeriOku_i("AktifTema")!=-1)
        {
            Material[] mats = _Renderer.materials;//materyalleri mats dizisine attýk.
            mats[0] = Materyaller[_BellekYonetim.VeriOku_i("AktifTema")];
            _Renderer.materials = mats;//tekrardan deðiþen materyal oludðu için yeniledik rendereri.
        }
        else
        {
            Material[] mats = _Renderer.materials;//materyalleri mats dizisine attýk.
            mats[0] = VarsayilanTema;
            _Renderer.materials = mats;//tekrardan deðiþen materyal oludðu için yeniledik rendereri.     
        }
    }
    public void CikisButonislem(string Durum)
    {
        Sesler[1].Play();
        Time.timeScale = 0;//oyunu durduracak
        if (Durum == "durdur")
        {
            islemPanelleri[0].SetActive(true);
        }
        else if (Durum == "devamet")
        {
            islemPanelleri[0].SetActive(false);
            Time.timeScale = 1;//devam
        }
        else if (Durum=="tekrar")
        {
            SceneManager.LoadScene(_Scene.buildIndex);
            Time.timeScale = 1;//devam
        }
        else if (Durum == "anasayfa")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;//devam
        }
    }
    public void Ayarlar(string Durum)
    {
        if (Durum=="ayarla")
        {
            islemPanelleri[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            islemPanelleri[1].SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SesiAyarla()
    {
        _BellekYonetim.VeriKaydet_float("OyunSes", OyunSesiAyar.value);
        Sesler[0].volume = OyunSesiAyar.value;
    }
    public void SonrakiLevel()
    {
        StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
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
}
