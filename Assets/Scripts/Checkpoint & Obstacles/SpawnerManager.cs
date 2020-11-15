using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script manages the multipurpose spawners under it's control
It will first deactivate all spawners
It will reactivate spawners when the player enters it's trigger bix
*/
public class SpawnerManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child != null)
                child.GetComponent<Spawner>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child != null)
                    child.GetComponent<Spawner>().enabled = true;
            }
        }
    }
}
