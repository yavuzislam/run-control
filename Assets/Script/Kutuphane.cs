using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEditor.PackageManager.Requests;

namespace yavuz
{
    public class Matematiksel_islemler
    {
        public void Carpma(int gelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int sayi = 0;
            int donguSayisi = (GameManager.AnlikKarakterSayisi * gelenSayi) - GameManager.AnlikKarakterSayisi;
            foreach (var item in Karakterler)
            {
                if (sayi < donguSayisi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item1 in OlusturmaEfektleri)
                        {
                            if (!item1.activeInHierarchy)
                            {
                                //Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                //yukar�da karakteri baz al�yorduk ama olu�turmada �arpt��� noktay� ele al�caz 
                                item1.SetActive(true);
                                item1.transform.position = Pozisyon.position;//burda �arp�lan nokta referans al�nd�
                                item1.GetComponent<ParticleSystem>().Play();
                                item1.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi *= gelenSayi;
        }
        public void Toplama(int gelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int sayi1 = 0;
            foreach (var item in Karakterler)
            {
                if (sayi1 < gelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item1 in OlusturmaEfektleri)
                        {
                            if (!item1.activeInHierarchy)
                            {
                                //Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                //yukar�da karakteri baz al�yorduk ama olu�turmada �arpt��� noktay� ele al�caz 
                                item1.SetActive(true);
                                item1.transform.position = Pozisyon.position;//burda �arp�lan nokta referans al�nd�
                                item1.GetComponent<ParticleSystem>().Play();
                                item1.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi1++;
                    }
                }
                else
                {
                    sayi1 = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi += gelenSayi;
        }
        public void Cikarma(int gelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi <= gelenSayi)
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item1 in YokOlmaEfektleri)
                    {
                        if (!item1.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                            item1.SetActive(true);
                            item1.transform.position = yeniPoz;
                            item1.GetComponent<ParticleSystem>().Play();
                            item1.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int sayi2 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi2 != gelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item1 in YokOlmaEfektleri)
                            {
                                if (!item1.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                    item1.SetActive(true);
                                    item1.transform.position = yeniPoz;
                                    item1.GetComponent<ParticleSystem>().Play();
                                    item1.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi2++;
                        }
                    }
                    else
                    {
                        sayi2 = 0;
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= gelenSayi;
            }
        }
        public void Bolme(int gelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            int donguSayisi = (GameManager.AnlikKarakterSayisi / gelenSayi) * (gelenSayi - 1);
            if (GameManager.AnlikKarakterSayisi <= gelenSayi)
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item1 in YokOlmaEfektleri)
                    {
                        if (!item1.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                            item1.SetActive(true);
                            item1.transform.position = yeniPoz;
                            item1.GetComponent<ParticleSystem>().Play();
                            item1.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != donguSayisi)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item1 in YokOlmaEfektleri)
                            {
                                if (!item1.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                    item1.SetActive(true);
                                    item1.transform.position = yeniPoz;
                                    item1.GetComponent<ParticleSystem>().Play();
                                    item1.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                if (GameManager.AnlikKarakterSayisi % gelenSayi == 0)
                {
                    GameManager.AnlikKarakterSayisi /= gelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % gelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= gelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
                else if (GameManager.AnlikKarakterSayisi % gelenSayi == 2)
                {
                    GameManager.AnlikKarakterSayisi /= gelenSayi;
                    GameManager.AnlikKarakterSayisi += 2;
                }

            }
        }
    }
    public class BellekYonetim
    {
        public void VeriKaydet_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
        public string VeriOku_s(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int VeriOku_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float VeriOku_f(string Key)//geriye deger d�nd�r�cek
        {
            return PlayerPrefs.GetFloat(Key);
        }
        public void KontrolEtveTanimla()//burda anahtaralara default de�erlerini at�yoruz
        {
            if (!PlayerPrefs.HasKey("SonLevel"))//Son level ad�nda bir anahtar daha �nce tan�ml� m�?
            {
                PlayerPrefs.SetInt("SonLevel", 5);//5 level1 sahnesinin indexi
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);
                PlayerPrefs.SetFloat("MenuSes", 1);
                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetString("Dil", "TR");
                PlayerPrefs.SetInt("GecisReklamiSayisi", 1);

            }
        }

    }
    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int Item_Index;
        public string Item_ad;
        public int Puan;
        public bool SatinAlmaDurumu;
    }
    public class VeriYonetimi
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {
            // _ItemBilgileri.Add(new ItemBilgileri());//burda ItemBilgileri s�n�f�n� bir listeye ekledik.
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");//itemverileri ad�nda dosya olu�turduk
            bf.Serialize(file, _ItemBilgileri);//dosyam�za verilerimiz i�indeki puan bilgisini buraya yaz.
            file.Close();//dosyay� kapat
        }
        List<ItemBilgileri> _ItemicListe;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))//b�yle bir dosya var m� kontrol ediliyor
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);//dosyay� a�
                _ItemicListe = (List<ItemBilgileri>)bf.Deserialize(file);//yukar�da object t�r�nde kay�t oldu i�mdi d�nerken int olmas� gerekiyor.
                file.Close();

                //Debug.Log(_ItemBilgileri[1].SatinAlmaDurumu);
            }
        }
        public List<ItemBilgileri> ListeyiAktar()
        {
            return _ItemicListe;
        }
        public void ilkKurulumDosyaOlusturma(List<ItemBilgileri> _ItemBilgileri, List<DilVerileriAnaObje> _DilVerileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))//kod blo�u sadece birkez �al��mas� sa�land�
            {
                // _ItemBilgileri.Add(new ItemBilgileri());//burda ItemBilgileri s�n�f�n� bir listeye ekledik.
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");//itemverileri ad�nda dosya olu�turduk
                bf.Serialize(file, _ItemBilgileri);//dosyam�za verilerimiz i�indeki puan bilgisini buraya yaz.
                file.Close();//dosyay� kapat
            }
            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))//kod blo�u sadece birkez �al��mas� sa�land�
            {
                // _ItemBilgileri.Add(new ItemBilgileri());//burda ItemBilgileri s�n�f�n� bir listeye ekledik.
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");//itemverileri ad�nda dosya olu�turduk
                bf.Serialize(file, _DilVerileri);//dosyam�za verilerimiz i�indeki puan bilgisini buraya yaz.
                file.Close();//dosyay� kapat
            }
        }
        //-----------------
        List<DilVerileriAnaObje> _DilVerileriicListe;
        public void Dil_Load()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))//b�yle bir dosya var m� kontrol ediliyor
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);//dosyay� a�
                _DilVerileriicListe = (List<DilVerileriAnaObje>)bf.Deserialize(file);//yukar�da object t�r�nde kay�t oldu i�mdi d�nerken int olmas� gerekiyor.
                file.Close();

                //Debug.Log(_ItemBilgileri[1].SatinAlmaDurumu);
            }
        }
        public List<DilVerileriAnaObje> DilVerileriListeyiAktar()
        {
            return _DilVerileriicListe;
        }
    }

    //---- Dil Y�netimi
    [Serializable]
    public class DilVerileriAnaObje
    {
        public List<DilVerileri_TR> _DilVerileri_TR = new List<DilVerileri_TR>();//a�a��daki s�n�fn� �rnekledik
        public List<DilVerileri_TR> _DilVerileri_EN = new List<DilVerileri_TR>();//ayn� yap�y� kulland��� i�in sadece �rnekleyip ad�n� de�i�tirdik.
    }
    [Serializable]
    public class DilVerileri_TR
    {
        public string Metin;
    }
    //---- Reklam Y�netimi
    public class ReklamYontem
    {
        private InterstitialAd interstitial;
        //Ge�is Reklam�
        public void RequestInterstitial()
        {
            string AdUnitId;
#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
                      AdUnitId="unexpected_platform";
#endif
            interstitial = new InterstitialAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);

            interstitial.OnAdClosed += GecisReklamiKapatildi;
        }
        void GecisReklamiKapatildi(object sender,EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }
        public void GecisReklamiGoster()
        {
            if (PlayerPrefs.GetInt("GecisReklamiSayisi")==2)
            {
                if (interstitial.IsLoaded())
                {
                    PlayerPrefs.SetInt("GecisReklamiSayisi", 1);
                    interstitial.Show();
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitial();
                }
            }
            else
            {
                PlayerPrefs.SetInt("GecisReklamiSayisi", PlayerPrefs.GetInt("GecisReklamiSayisi")+1);
            }
        }
        //�d�ll� Reklam
    }

}


