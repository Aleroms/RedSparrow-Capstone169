using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCan : MonoBehaviour
{
    [SerializeField]
    private int ammoHeld;
    [SerializeField]
    private int ammoType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
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
