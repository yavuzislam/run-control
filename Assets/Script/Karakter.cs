using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public Kamera _Kamera;
    public bool SonaGeldikmi;
    public GameObject hedef;
    public Slider _Slider;
    public GameObject GecisNoktasi;
    private void FixedUpdate()
    {
        if (!SonaGeldikmi)
        {
            transform.Translate(Vector3.forward * .5f * Time.deltaTime);
        }
    }
    private void Start()
    {
        float Fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
        _Slider.maxValue = Fark;
    }
    void Update()
    {
        if (Time.timeScale != 0)//ayarlarda ses ayarý yaparken karakter kaymasý vardý böyle engellendi.
        {
            if (SonaGeldikmi)
            {
                transform.position = Vector3.Lerp(transform.position, hedef.transform.position, .075f);
                if (_Slider.value != 0)
                {
                    _Slider.value -= .01f;
                }
            }
            else
            {
                float Fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
                _Slider.value = Fark;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y,
                            transform.position.z), .5f);
                    }
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y,
                            transform.position.z), .5f);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cikarma") || other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            _GameManager.AdamYonetim(other.tag, sayi, other.transform);
        }
        else if (other.CompareTag("SonTetikleyici"))
        {
            _Kamera.SonaGeldikmi = true;
            _GameManager.DusmanlariTetikle();
            SonaGeldikmi = true;
        }
        else if (other.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject);//boskarakteri karakterler listesine ekledik
        }
    }
    private void OnCollisionEnter(Collision collision)//temas ettiði müddetçe
    {
        if (collision.gameObject.CompareTag("Direk") || collision.gameObject.CompareTag("igneliKutu")
            || collision.gameObject.CompareTag("Pervaneigneler"))
        {
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            }

        }
    }
}
