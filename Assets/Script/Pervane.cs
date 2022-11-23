using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _Animator;
    public float BeklemeSuresi;
    public BoxCollider _Ruzgar;//kullanýcý rüzgaralaný adlý nesneyi pervane adný nesnenin yazýlým alalnýndaki yere atýcak
    public void AnimasyonDurum(string durum)
    {
        if (durum == "true")
        {
            _Animator.SetBool("Calistir", true);
            _Ruzgar.enabled = true;//Rüzgaralaný adlý objenin box colliderý aktif olucak yani rüzgar ile itme için gerekli
        }
        else
        {
            _Animator.SetBool("Calistir", false);
            _Ruzgar.enabled = false;
            StartCoroutine(AnimasyonTetikle());//zamanlayýcýyý çalýþtýrdý
        }

    }
    IEnumerator AnimasyonTetikle()//zamanlayýcý olarak kullandýk
    {
        yield return new WaitForSeconds(BeklemeSuresi);//kullanýcýnýn girdiði deðere göre sayacak
        AnimasyonDurum("true");//zaman bitince bu fonksiyonu çalýþtýracak tekrar true olduðu için pervane dönmeye baþlayacak
    }
}
