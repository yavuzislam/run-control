using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(Pasiflestir());             A�a��daki fonksiyonla ayn� g�revi yap�yor.
    }
    IEnumerator Pasiflestir()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }*/
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
