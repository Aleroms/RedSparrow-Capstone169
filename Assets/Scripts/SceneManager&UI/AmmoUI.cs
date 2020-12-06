using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    //[SerializeField]
    //private GameObject player;//This is where the PlayerStatTrack script will be held
    private PlayerStatTrack playerStatTrack; //The playerStatTrack script attached to the player
    private GunController gun; //If the player is holding a gun, its script will go here
    [SerializeField]
    private Text ammoText; //The component that will show the ammo count in the UI
    [SerializeField]
    private Text secondary;
    private InventoryController inventory;
    private string ammoCount, maxCount;

    void Start()
    {   //current code was causing too many errors if developers did not link player gameobject to AmmoUI
        //better way is to GameObject.Find or FindGameObjectWithTag
        playerStatTrack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatTrack>();//player.GetComponent<PlayerStatTrack>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();//player.GetComponent<InventoryController>();
        ammoText = transform.GetChild(0).gameObject.GetComponent<Text>();
        secondary = transform.GetChild(1).gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        //check to see if a player has a gun. If they do set gun  equal to the GunController of the gun the player currently has.
        //Then check the gun type and display the corresponding amount of ammo the player has for that gun.
        if (playerStatTrack.getHasGun1())
        {

            gun = inventory.primaryGun();
            if (gun.getAmmoCount() > 0)
                ammoCount = "<color=#FFFFFF>" + gun.getAmmoCount().ToString() + "</color>";
            else
                ammoCount = "<color=#FF0000>" + gun.getAmmoCount().ToString() + "</color>";
            //ammoText.text = gun.gunType + " <color=#CCCC00>" + gun.getAmmoCount() + "</color>/" + playerStatTrack.getLittleAmmoPool();
            if (gun.getGunType() == 1)
            {
                ammoText.text = "Pistol " + ammoCount + "/" + playerStatTrack.getLittleAmmoPool();
            }
            else if (gun.hasSGunType()  && gun.getGunType() == 2)
            {
                ammoText.text = "LMG " + ammoCount + "/" + playerStatTrack.getLargeAmmoPool();
            }
            else if (gun.hasSGunType() && gun.getGunType() == 3)
            {
                ammoText.text = "LMG  " + ammoCount + "/" + playerStatTrack.getLaserAmmoPool();
            }
            else if (gun.getGunType() == 3)
            {
                ammoText.text = "Sniper " + ammoCount + "/" + playerStatTrack.getLaserAmmoPool();
            }
        }
        else if (!playerStatTrack.getHasGun1())

        {
            ammoText.text = "None";
        }
        if (inventory.hasGun2())
        {
            if (inventory.secondgun().getAmmoCount() > 0)
                ammoCount = "<color=#FFFFFF>" + inventory.secondgun().getAmmoCount().ToString() + "</color>";
            else
                ammoCount = "<color=#FF0000>" + inventory.secondgun().getAmmoCount().ToString() + "</color>";
            if (inventory.secondgun().getGunType() == 1)
            {
                //secondary.text = "On hold: Pistol";
                secondary.text = "Pistol " + ammoCount + "/" + playerStatTrack.getLittleAmmoPool();
            }
            else if (inventory.secondgun().getGunType() == 2 && inventory.secondgun().hasSGunType())
            {
                secondary.text = "LMG " + ammoCount + "/" + playerStatTrack.getLargeAmmoPool();
            }
            else if (inventory.secondgun().getGunType() == 3 && inventory.secondgun().hasSGunType())
            {
                secondary.text = "LMG  " + ammoCount + "/" + playerStatTrack.getLaserAmmoPool();
            }
            else if (inventory.secondgun().getGunType() == 3)
            {
                secondary.text = "Sniper " + ammoCount + "/" + playerStatTrack.getLaserAmmoPool();
            }
            

        }
        else if(!inventory.hasGun2())
        {
            secondary.text = "None";
        }
       
    }
}
