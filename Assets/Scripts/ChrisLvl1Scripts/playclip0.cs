using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playclip0 : MonoBehaviour
{
    public GodHand gh;
    public GameObject balls;

    private void Update()
    {
        if (gh.triggerChecker == 1) {
            balls.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gh.playClip(0);
    }
}
