using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playClip5 : MonoBehaviour
{
    public GodHand gh;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            gh.playClip(5);
        }
    }
}
