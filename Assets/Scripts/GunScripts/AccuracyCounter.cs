using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyCounter : MonoBehaviour
{
    public int shotsFired, shotsHit, accuracy;
    public Text accuracyCounter;
    public Image reticlePlayer;
    private Camera aimCamera;
    // Start is called before the first frame update
    void Start()
    {
        accuracyCounter = GameObject.Find("Accuracy Counter").GetComponent<Text>();
        reticlePlayer = GameObject.Find("Reticle").GetComponent<Image>();
        aimCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shotsFired > 0)
        {
            accuracy = (int)(((float)shotsHit / shotsFired) * 100);
            accuracyCounter.text = "Accuracy: " + accuracy + "%";
        }
        Vector3 rayOrigin = aimCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));//Origin of the ray is center of the screen
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, aimCamera.transform.forward, out hit)) //If we hit something...
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
                reticlePlayer.color = Color.red;
            else
                reticlePlayer.color = Color.white;
        }
        else
            reticlePlayer.color = Color.white;
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
