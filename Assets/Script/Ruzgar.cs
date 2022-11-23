using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruzgar : MonoBehaviour
{
    private void OnTriggerStay(Collider other)//yani etkile�imde kald��� s�rece bu fonksiyon �al��acak.
    {
        if (other.CompareTag("AltKarakter"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 0, 0), ForceMode.Impulse);
        }
    }
}
