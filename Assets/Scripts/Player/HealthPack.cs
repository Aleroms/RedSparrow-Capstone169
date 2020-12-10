using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField]
    private int value = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<Rigidbody>().isKinematic) // simulated gravity
            GetComponent<Rigidbody>().isKinematic = true;
        if (other.tag == "Player" && other.GetComponent<Player>().GetCurrentHealth() < other.GetComponent<Player>().GetMaxHealth())
        {
            if (other.GetComponent<Player>().GetCurrentHealth() + value > other.GetComponent<Player>().GetMaxHealth())
                other.GetComponent<Player>().resethealth();
            else
                other.GetComponent<Player>().Damage(-value);
            other.GetComponent<Player>().Damage(0);
            Destroy(gameObject);
        }
    }
}
