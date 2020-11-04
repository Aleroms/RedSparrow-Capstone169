using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of all player stats 
public class PlayerStatTrack : MonoBehaviour
{
    [System.Serializable]
    private class Stats
    {
        public int littleAmmoPool;//This ammo pool is for pistols, smgs, and other small caliber guns
        public int largeAmmoPool;// This ammo pool is for snipers, assualt rifles, and other large caliber guns
        public int laserAmmoPool;// This ammo pool is for laser type guns
        public bool hasGun;//This tells the game if the player already has a gun
    }

    [SerializeField]
    Stats playerStats = new Stats();

    public int getLittleAmmoPool() {
        return playerStats.littleAmmoPool;
    }
    public int getLargeAmmoPool()
    {
        return playerStats.largeAmmoPool;
    }
    public int getLaserAmmoPool()
    {
        return playerStats.laserAmmoPool;
    }
    public bool getHasGun() {
        return playerStats.hasGun;
    }
    public void setLittleAmmoPool(int input) {
        playerStats.littleAmmoPool = input;
    }
    public void setLargeAmmoPool(int input)
    {
        playerStats.largeAmmoPool = input;
    }
    public void setLaserAmmoPool(int input)
    {
        playerStats.laserAmmoPool = input;
    }
    public void setHasGun(bool input) {
        playerStats.hasGun = input;
    }
}
