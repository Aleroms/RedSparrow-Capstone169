using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyCounter : MonoBehaviour
{
    public int shotsFired, shotsHit, accuracy;
    public Text accuracyCounter;
    // Start is called before the first frame update
    void Start()
    {
        accuracyCounter = GameObject.Find("Accuracy Counter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shotsFired > 0)
        {
            accuracy = (int)(((float)shotsHit / shotsFired) * 100);
            accuracyCounter.text = "Accuracy: " + accuracy + "%";
        }
        
    }
    public void ShotsFired()
    {
        ++shotsFired;
    }
    public void ShotsHit()
    {
        ++shotsHit;
    }
}
