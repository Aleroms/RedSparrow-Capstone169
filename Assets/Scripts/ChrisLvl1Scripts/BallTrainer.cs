using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrainer : MonoBehaviour
{
    Transform[] ts;
    public GodHand gh;
    public GameObject platform;
    private bool playedclip = false;
    // Update is called once per frame
    void Update()
    {
        ts = gameObject.GetComponentsInChildren<Transform>();
        if (ts.Length == 1 && !playedclip) {
            gh.playClip(1);
            playedclip = true;
        }
        if (gh.triggerChecker == 2) {
            Destroy(platform);
            gameObject.SetActive(false);
        }
    }
}
