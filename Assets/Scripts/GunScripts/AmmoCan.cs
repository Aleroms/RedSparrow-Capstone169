using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCan : MonoBehaviour
{
    [SerializeField]
    private int ammoHeld;//How much ammo is in the ammo can
    [SerializeField]
    private int ammoType;//Which ammo type is in the ammo can

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<Rigidbody>().isKinematic) // simulated gravity
            GetComponent<Rigidbody>().isKinematic = true;
        if (other.tag == "Player") {
            print("ControllerColliderHit!");
            if (ammoType == 1)
            {
                other.GetComponent<PlayerStatTrack>().setLittleAmmoPool(other.GetComponent<PlayerStatTrack>().getLittleAmmoPool() + ammoHeld);
            }
            else if (ammoType == 2)
            {
                other.GetComponent<PlayerStatTrack>().setLargeAmmoPool(other.GetComponent<PlayerStatTrack>().getLargeAmmoPool() + ammoHeld);
            }
            else {
                other.GetComponent<PlayerStatTrack>().setLaserAmmoPool(other.GetComponent<PlayerStatTrack>().getLaserAmmoPool() + ammoHeld);
            }
            Destroy(gameObject);
        }
    }
}
