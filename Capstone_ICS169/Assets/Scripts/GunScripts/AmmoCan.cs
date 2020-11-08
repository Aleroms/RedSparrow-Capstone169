using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCan : MonoBehaviour
{
    [SerializeField]
    private int ammoHeld;//How much ammo is in the ammo can
    [SerializeField]
    private int ammoType;//Which ammo type is in the ammo can

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            print("ControllerColliderHit!");
            if (ammoType == 1)
            {
                collision.gameObject.GetComponent<PlayerStatTrack>().setLittleAmmoPool(collision.gameObject.GetComponent<PlayerStatTrack>().getLittleAmmoPool() + ammoHeld);
            }
            else if (ammoType == 2)
            {
                collision.gameObject.GetComponent<PlayerStatTrack>().setLargeAmmoPool(collision.gameObject.GetComponent<PlayerStatTrack>().getLargeAmmoPool() + ammoHeld);
            }
            else {
                collision.gameObject.GetComponent<PlayerStatTrack>().setLaserAmmoPool(collision.gameObject.GetComponent<PlayerStatTrack>().getLaserAmmoPool() + ammoHeld);
            }
            Destroy(gameObject);
        }
    }
}
