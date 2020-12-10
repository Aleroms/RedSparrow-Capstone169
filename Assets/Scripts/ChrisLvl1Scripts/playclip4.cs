using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playclip4 : MonoBehaviour
{
    public GameObject wind1;
    public GameObject wind2;
    public GameObject[] walls;
    public GodHand gh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            wind1.SetActive(false);
            wind2.SetActive(true);
            foreach (GameObject w in walls) {
                w.SetActive(false);
            }
            gh.playClip(4);
            gameObject.SetActive(false);
        }
    }
}
