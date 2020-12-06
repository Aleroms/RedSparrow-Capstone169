using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolPractice : MonoBehaviour
{
    [SerializeField]
    GameObject bridge1;
    [SerializeField]
    GameObject bridge2;
    [SerializeField]
    GameObject bridge3;
    [SerializeField]
    GameObject target1;

    [SerializeField]
    GameObject target2;

    [SerializeField]
    GameObject target3;

    [SerializeField]
    GameObject target4;

    [SerializeField]
    GameObject target5;

    [SerializeField]
    GameObject target6;
    // Start is called before the first frame update
    void Start()
    {
        bridge1.SetActive(false);
        bridge2.SetActive(false);
        bridge3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(target1 == null && target2 == null)
        {
            bridge1.SetActive(true);
        }
        if (target3 == null && target4 == null)
        {
            bridge2.SetActive(true);
        }
        if (target5 == null && target6 == null)
        {
            bridge3.SetActive(true);
        }
    }
}
