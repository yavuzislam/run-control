using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public GameObject Saldiri_Hedefi;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameManager _GameManager;
    bool Saldiri_basladimi;
    public void AnimasyonTetikle()
    {
        _Animator.SetBool("Saldir", true);
        Saldiri_basladimi = true;
    }
    private void LateUpdate()
    {
        if (Saldiri_basladimi)
        {
            _Navmesh.SetDestination(Saldiri_Hedefi.transform.position);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakter"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, .23f, transform.position.z);
            _GameManager.YokOlmaEfektiOlustur(yeniPoz, false, true);
            gameObject.SetActive(false);
        }
    }
}
