using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _Animator;
    public float BeklemeSuresi;
    public BoxCollider _Ruzgar;//kullan�c� r�zgaralan� adl� nesneyi pervane adn� nesnenin yaz�l�m alaln�ndaki yere at�cak
    public void AnimasyonDurum(string durum)
    {
        if (durum == "true")
        {
            _Animator.SetBool("Calistir", true);
            _Ruzgar.enabled = true;//R�zgaralan� adl� objenin box collider� aktif olucak yani r�zgar ile itme i�in gerekli
        }
        else
        {
            _Animator.SetBool("Calistir", false);
            _Ruzgar.enabled = false;
            StartCoroutine(AnimasyonTetikle());//zamanlay�c�y� �al��t�rd�
        }

    }
    IEnumerator AnimasyonTetikle()//zamanlay�c� olarak kulland�k
    {
        yield return new WaitForSeconds(BeklemeSuresi);//kullan�c�n�n girdi�i de�ere g�re sayacak
        AnimasyonDurum("true");//zaman bitince bu fonksiyonu �al��t�racak tekrar true oldu�u i�in pervane d�nmeye ba�layacak
    }
}
