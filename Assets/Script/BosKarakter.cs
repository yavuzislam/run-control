using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material AtanacakMateryal;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject Target;
    public GameManager _GameManager;

    bool Temasvar;
    private void LateUpdate()
    {
        if (Temasvar)
            _Navmesh.SetDestination(Target.transform.position);
    }
    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Karakter") || other.CompareTag("AltKarakter"))
        {
            if (gameObject.CompareTag("BosKarakter"))//bunu yaparak etiketi deðiþtiðinde anlýk karakter sayýsýný arttýrmasýný engelledik
            {
                MaterialdegistirveAnimasyonTetikle();
                Temasvar = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else if (other.CompareTag("igneliKutu"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Testere"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Pervaneigneler"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Balyoz"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Dusman"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), false, false);
            gameObject.SetActive(false);
        }
    }
    void MaterialdegistirveAnimasyonTetikle()
    {
        Material[] mats = _Renderer.materials;//materyalleri mats dizisine attýk.
        mats[0] = AtanacakMateryal;
        _Renderer.materials = mats;//tekrardan deðiþen materyal oludðu için yeniledik rendereri.
        _Animator.SetBool("Saldir", true);
        GameManager.AnlikKarakterSayisi++;
        Debug.Log(GameManager.AnlikKarakterSayisi);
        gameObject.tag = "AltKarakter";
    }

}
