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
        public bool hasGun1;//This tells the game if the player already has a gun in slot 1
        public bool hasGun2;//This tells the game if the player already has a gun in slot 2
        public int MissionKills;
        public float mouseSensitivity;
        public float volumeLevel;
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
    public bool getHasGun1() {
        return playerStats.hasGun1;
    }
    public bool getHasGun2()
    {
        return playerStats.hasGun2;
    }
    public int getKillCount()
    {
        return playerStats.MissionKills;
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
    public void setHasGun1(bool input) {
        playerStats.hasGun1 = input;
    }
    public void setHasGun2(bool input)
    {
        playerStats.hasGun2 = input;
    }
    public void addKill() {
        playerStats.MissionKills += 1;
    }
    public void resetKills() {
        playerStats.MissionKills = 0;
    }




    public void setMouseSensitivity(float input) 
    {
        PlayerPrefs.SetFloat("mouseSensitivity", input);
    }
    public void setVolumeLevel(float input)
    {
        PlayerPrefs.SetFloat("volumeLevel", input);
    }
    public float getMouseSensitivity()
    {
        return PlayerPrefs.GetFloat("mouseSensitivity", 3.2f);
    }
    public float getVolumeLevel()
    {
        return PlayerPrefs.GetFloat("volumeLevel", 70f);
    }
}
