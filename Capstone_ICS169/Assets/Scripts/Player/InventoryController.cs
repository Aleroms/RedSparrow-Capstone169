using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;//This is the player

    [SerializeField]//Gun1 and 2 are serialized for debugging only. they should not be assigned at start
    private GameObject gun1;//This is the gun in slot 1
    [SerializeField]
    private GameObject gun2;//This is the gun in slot 2

    void Update()
    {
        if (Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getSwitchGunsKey()) && gun2 != null) {
            gunSwitch();
        }
        if (gun1 == null && gun2 != null) {
            gun1 = gun2;
            gun1.SetActive(true);
            gun2 = null;
            player.GetComponent<PlayerStatTrack>().setHasGun1(true);
            player.GetComponent<PlayerStatTrack>().setHasGun2(false);
        }
    }

    public void setGun1(GameObject gun) {
        gun1 = gun;
    }

    public void setGun2(GameObject gun) {
        gun2 = gun;
        gun2.SetActive(false);
    }

    void gunSwitch() {
        GameObject temp = gun1;
        gun1 = gun2;
        gun2 = temp;
        gun1.SetActive(true);
        gun2.SetActive(false);
    }
}
