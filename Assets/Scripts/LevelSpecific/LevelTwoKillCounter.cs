using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwoKillCounter : MonoBehaviour
{
    public int[] currentKills = new int[3];
    public int[] targetKills = new int[3];
    public int phase;
    public Text killCounter;
    // Start is called before the first frame update
    void Start()
    {
        killCounter = GameObject.Find("Kill Counter").GetComponent<Text>();
        //targetKills[0] = 3;
        //targetKills[1] = 3;
        //targetKills[2] = 20;
        killCounter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; ++i)
            if (currentKills[i] == targetKills[i] && phase == i)
            {
                ++phase;
                killCounter.enabled = false;
            }
        for (int i = 0; i < 3; ++i)
            if (currentKills[i] > 0 && phase == i && killCounter.enabled == false)
            {
                //++phase;
                killCounter.enabled = true;
            }
        for (int i = 0; i < 3; ++i)
            if (phase == i)
            {
                killCounter.text = currentKills[i] + "/" + targetKills[i] + " Targets Killed";
            }
    }

    public void DetectKill()
    {
        for (int i = 0; i < 3; ++i)
            if (currentKills[i] < targetKills[i] && phase == i)
                ++currentKills[i];
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
