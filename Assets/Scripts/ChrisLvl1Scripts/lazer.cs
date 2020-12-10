using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    public Camera aimCamera;// This will be the origin point for the ray cast. 

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = aimCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));//Origin of the ray is center of the screen
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, aimCamera.transform.forward, out hit, 50)) //If we hit something...
        {
            if (hit.collider.CompareTag("Enemy")) {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
