using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public CharacterController player;
    public Vector3 windDirection = Vector3.forward;
    public float windStrength = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null) {
            player.Move(windDirection * windStrength);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            player = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
    }
}
