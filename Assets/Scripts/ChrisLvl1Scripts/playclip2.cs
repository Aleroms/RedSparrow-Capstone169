using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playclip2 : MonoBehaviour
{
    public GodHand gh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().enabled = true;
            gh.playClip(2);
            gameObject.SetActive(false);
        }
    }
}
