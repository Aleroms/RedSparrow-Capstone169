using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneLights : MonoBehaviour
{
    [SerializeField]
    private GameObject DomeBridge;
    [SerializeField]
    private GameObject DomePlatform;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        if(DomePlatform.activeSelf)
        {
            DomePlatform.SetActive(false);
        }
        if(!DomeBridge.activeSelf)
        {
            DomeBridge.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DomeBridge.SetActive(false);
            DomePlatform.SetActive(true);
        }
    }
}
