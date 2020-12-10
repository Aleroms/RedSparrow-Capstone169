using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodHand : MonoBehaviour
{
    public AudioClip[] voiceLines;
    public AudioSource aSource;
    public int triggerChecker;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        triggerChecker = 0;
        Player.GetComponent<Player>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerChecker == 1)
        {
            Player.GetComponent<lazer>().enabled = true;
        }
        else if (triggerChecker == 2) {
            Player.GetComponent<lazer>().enabled = false;
            Player.GetComponent<Player>().enabled = true;
        }
    }

    IEnumerator playClip()
    {
        aSource.Play();
        yield return new WaitWhile(() => aSource.isPlaying);
        triggerChecker++;
    }

    public void playClip(int clipNum) {
        aSource.clip = voiceLines[clipNum];
        StartCoroutine(playClip());
    }
}
