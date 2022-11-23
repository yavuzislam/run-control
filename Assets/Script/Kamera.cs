using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public bool SonaGeldikmi;
    public GameObject GidecegiYer;
    void Start()
    {
        target_offset = transform.position - target.transform.position;
        //kameram�n pozisyonu ile hedef aras�daki fark� ald�k
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!SonaGeldikmi)////kameram�n pozisyonunu karakterin pozisyonuna getir(target offset kadar farkla takip ediyor)
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
        else
            transform.position = Vector3.Lerp(transform.position, GidecegiYer.transform.position, .045f);
    }
}
