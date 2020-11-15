﻿
using UnityEngine;

/*This script is for the multipurpose spawner
We need 3 things
A game object to spawn
How many times that game object will spawn
How much delay is there between spawns
*/
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnedItem;//This is the game object we will be spawning
    [SerializeField]
    private int numOfSpawns = -1;//This is how many times we will spawn ^. A negative number means we spawn an infinte amount
    [SerializeField]
    private float spawnTimeDelay = 0f;//This is the delay between spawns. A number <= 0 means there will be no delay

    private float nextSpawnTime = 0f;

    void Update()
    {
        //If there is a positive number of spawns and the delay is over
        if (numOfSpawns > 0 && Time.time > nextSpawnTime) {
            Instantiate(spawnedItem, transform.position, transform.rotation);//Spawn the item
            setCoolDown();//Set the delay
            numOfSpawns -= 1;//Subtract 1 from the number of spawnds
        }
        //If there is a negative number of spawnds and the delay is over
        else if (numOfSpawns < 0 && Time.time > nextSpawnTime) {
            Instantiate(spawnedItem, transform.position, transform.rotation);//Spawn the item
            setCoolDown();//And set the delay
        }
        //As you can see, if the number of spawns is 0, then nothing will happen
    }
    void setCoolDown() {
        nextSpawnTime = Time.time + spawnTimeDelay;
    }
}