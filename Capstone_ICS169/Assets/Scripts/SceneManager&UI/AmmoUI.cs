using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;//This is where the PlayerStatTrack script will be held
    private PlayerStatTrack playerStatTrack; //The playerStatTrack script attached to the player
    private GunController gun; //If the player is holding a gun, its script will go here
    private TextMeshProUGUI ammoText; //The component that will show the ammo count in the UI


    void Start()
    {
        ammoText = gameObject.GetComponent<TextMeshProUGUI>();
        playerStatTrack = player.GetComponent<PlayerStatTrack>();
    }
    // Update is called once per frame
    void Update()
    {
        //check to see if a player has a gun. If they do set gun  equal to the GunController of the gun the player currently has.
        //Then check the gun type and display the corresponding amount of ammo the player has for that gun.
        if (playerStatTrack.getHasGun())
        {

            gun = player.GetComponentInChildren<GunController>();

            if(gun.getGunType() == 1)
            {
                ammoText.text = gun.getAmmoCount() + "/" + playerStatTrack.getLittleAmmoPool();
            }
            else if (gun.getGunType() == 2)
            {
                ammoText.text = gun.getAmmoCount() + "/" + playerStatTrack.getLargeAmmoPool();
            }
            else if (gun.getGunType() == 3)
            {
                ammoText.text = gun.getAmmoCount() + "/" + playerStatTrack.getLaserAmmoPool();
            }
        }
    }
}
