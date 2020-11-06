using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public bool isOn;
    public GameObject player, target;
    public Transform position;
    public float randomTime;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        position = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            transform.LookAt(position); // look at target
            if (randomTime <= 0) // move to random position
            {
                randomTime = Random.Range(0.5f, 2f);
                //print(randomTime);
                rb.velocity = new Vector3(2, -1, 2);
            }

            randomTime -= Time.deltaTime;
        }
        
    }
}
