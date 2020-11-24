using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesLeft : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesLeft;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        livesLeft.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().livesLeft() + " Lives Left";
    }
}
