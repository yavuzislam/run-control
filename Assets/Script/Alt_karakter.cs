using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alt_karakter : MonoBehaviour
{
    NavMeshAgent _Navmesh;
    public GameManager _GameManager;
    public GameObject Target;//arayüzden varis noktasini kendimiz atýcaz bu yüzden gamemanagerdan varis noktasi buraya tasindi
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);
    }
    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("igneliKutu"))
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
        else if (other.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject);//boskarakteri karakterler listesine ekledik
        }
    }
}
