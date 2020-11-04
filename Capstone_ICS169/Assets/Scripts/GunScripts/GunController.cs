using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private ShootingHitscan hitScanScript;//If the gun is using it, the ShootingHitscan script goes here
    [SerializeField]
    private ShootingPhysical bulletScript;//If the gun is using it, the ShootingPhysical script goes here
    [SerializeField]
    private Rigidbody gunRB;//This is the gun's rigid body
    [SerializeField]
    private BoxCollider gunBC;//This is the gun's box collider
    [SerializeField]
    private GameObject player;//This is the player
    [SerializeField]
    private Transform hand;//We need to know where the hand is 
    [SerializeField]
    private Transform playerCamera;//We also need to player's camera for when we drop the gun
    [SerializeField]
    private float pickUpRange;//How close do we need to be before we can pick up the gun
    [SerializeField]
    private float dropForwardForce;//How hard forawrd we wanna throw the gun
    [SerializeField]
    private float dropUpwardForce;//How hard up we wanna throw the gun
    [SerializeField]
    private int maxAmmoCount;//How many bullets can the gun have
    [SerializeField]
    private int PgunType;//This keeps track of the primary (P) fire mode's bullet type. 1= Little bullets, 2= Large bullets, 3= lasers
    [SerializeField]
    private int Sguntype;//This keeps track of the secondary (S) fire mode's bullet type

    public bool isEquipped;//Is the gun currently equiped?
    public int ammoCount;// How many bullets does the gun have right now

    void Start()
    {
        ammoCount = Random.Range(0, maxAmmoCount+1);// First, we will give the gun a random amount of ammo (Ammo cant exceed the max set earlier)
        if (!isEquipped) { // If the gun is not equiped, certain things must be set false, they will be explained below
            setThingsFalse();
        }
        else {//If the gun is equiped, then certain things will be set to true
            setThingsTrue();
        }
    }

    void Update()
    {
        Vector3 distToPlayer = player.transform.position - transform.position;//Every update, we wanna know how far away the player is to the gun
        //If the gun is not equiped, and the player is close enough to equip it, and the player presses the key to equip the gun, AND the player isn't already holding a gun
        if (!isEquipped && distToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getPickUp()) && !player.GetComponent<PlayerStatTrack>().getHasGun()) {
            PickUp();//Then pick up the gun
        }
        //If the player has a gun and presses the key to drop the gun
        if (isEquipped && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getDrop()))
        {
            Drop();//Then drop the gun
        }
        //If the player has a gun and presses the key to switch ammo type
        if(isEquipped && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getSwitchFireType())){
            SwitchFireType();//Then switch the ammo type
        }
        //If the player has a gun and presses the key to reolad the gun
        if (isEquipped && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getReloadKey())) {
            reload();//Then reload the gun....
        }
    }

    //When we pick up a gun...
    void PickUp() {
        gameObject.tag = "Player";//We set the gun's tag to "Player"
        setThingsTrue();//We set some things true
        transform.SetParent(hand);// We set the parent to the hand
        //We center the gun onto the hand
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

    //When we drop a gun
    void Drop() {
        gameObject.tag = "Untagged";//We set the tag to "Untagged"
        setThingsFalse();//Some things are set to false
        transform.SetParent(null);//The hand is no longer the parent
        //And we add forces to toss the gun away
        gunRB.AddForce(playerCamera.forward * dropForwardForce, ForceMode.Impulse);
        gunRB.AddForce(playerCamera.forward * dropUpwardForce, ForceMode.Impulse);
    }

    //Here is an explaination of things we set to false
    void setThingsFalse() {
        isEquipped = false;//The gun is not equiped
        player.GetComponent<PlayerStatTrack>().setHasGun(false);//We tell the player stat tracker that there is no longer a gun equiped
        gunRB.isKinematic = false;//We turn off the kunematic body
        gunBC.isTrigger = false;//The collision box is no longer a trigger box (The gun can be kicked around now)
        //Finally, we disable the shooting scripts
        if (hitScanScript != null)
        {
            hitScanScript.enabled = false;
        }
        if (bulletScript != null)
        {
            bulletScript.enabled = false;
        }
    }

    //For setting things true, we undo all the things we set false ^
    //Note when we pick up a weapon and it can shoot bullets and lasers, it will be set to shoot lasers first
    //So if a gun can shoot lasers, the PgunType should always be set to "3" 
    void setThingsTrue() {
        isEquipped = true;
        player.GetComponent<PlayerStatTrack>().setHasGun(true);
        gunRB.isKinematic = true;
        gunBC.isTrigger = true;
        if (hitScanScript != null)
        {
            hitScanScript.enabled = true;
        }
        else if (bulletScript != null)
        {
            bulletScript.enabled = true;
        }
    }

    //When switching fire types
    void SwitchFireType() {
        //First we make sure there are scripts to shoot bullets and lasers
        if (hitScanScript != null && bulletScript != null) {
            //If both exist, turn one off and enable the other
            if (hitScanScript.enabled == true) {
                hitScanScript.enabled = false;
                bulletScript.enabled = true;
            }
            else if (bulletScript.enabled == true) {
                hitScanScript.enabled = true;
                bulletScript.enabled = false;
            }
            //Then we change the ammo type being fired
            int temp = PgunType;
            PgunType = Sguntype;
            Sguntype = temp;
        }
    }

    //Every time we shoot...
    public void decreaseAmmo() {
        ammoCount -= 1;// ammo needs to be decreased
        //If we run out of ammo, we cant shoot
        if (ammoCount <= 0) {
            if (hitScanScript != null)
            {
                hitScanScript.enabled = false;
            }
            if (bulletScript != null)
            {
                bulletScript.enabled = false;
            }
        }
    }

    //This might be used for ammo ui so I put this here
    public int getAmmoCount() {
        return ammoCount;
    }

    //When you reload///
    void reload() {
        int ammoNeeded = maxAmmoCount - ammoCount;//First we need to know how much ammo we need
        int ammoGotten = 0;
        //Next we figure out which type of ammo we need
        if (PgunType == 1)// if 1, we need little bullets
        {
            if ((player.GetComponent<PlayerStatTrack>().getLittleAmmoPool() != 0)) {//First make sure we have some little bullets
                if (((player.GetComponent<PlayerStatTrack>().getLittleAmmoPool() - ammoNeeded) >= 0))//If we do and it amount is more than we need...
                {
                    ammoGotten = ammoNeeded;//The amount of ammo we got is the amount of ammo we need
                }
                else {
                    ammoGotten = player.GetComponent<PlayerStatTrack>().getLittleAmmoPool();//If the ammout is less than we need, the ammount of ammo we got is the amount of ammo we have left
                }
                player.GetComponent<PlayerStatTrack>().setLittleAmmoPool(player.GetComponent<PlayerStatTrack>().getLittleAmmoPool() - ammoGotten);//Finally we change the player's ammo pool to show we took ammo way
            }
        }
        else if (PgunType == 2)// if 3, we need large bullets
        {
            if ((player.GetComponent<PlayerStatTrack>().getLargeAmmoPool() != 0))
            {
                if (((player.GetComponent<PlayerStatTrack>().getLargeAmmoPool() - ammoNeeded) >= 0))
                {
                    ammoGotten = ammoNeeded;
                }
                else
                {
                    ammoGotten = player.GetComponent<PlayerStatTrack>().getLargeAmmoPool();
                }
                player.GetComponent<PlayerStatTrack>().setLargeAmmoPool(player.GetComponent<PlayerStatTrack>().getLargeAmmoPool() - ammoGotten);
            }
        }
        else {// else PgunType has to be 3, and we need laser bullets
            if ((player.GetComponent<PlayerStatTrack>().getLaserAmmoPool() != 0))
            {
                if (((player.GetComponent<PlayerStatTrack>().getLaserAmmoPool() - ammoNeeded) >= 0))
                {
                    ammoGotten = ammoNeeded;
                }
                else
                {
                    ammoGotten = player.GetComponent<PlayerStatTrack>().getLaserAmmoPool();
                }
                player.GetComponent<PlayerStatTrack>().setLaserAmmoPool(player.GetComponent<PlayerStatTrack>().getLaserAmmoPool() - ammoGotten);
            }
        }
        ammoCount += ammoGotten;//Finally, we add the ammo we got to the ammo count
        //And the gun can shoot again 
        if (hitScanScript != null && PgunType == 3)
        {
            hitScanScript.enabled = true;
        }
        if (bulletScript != null && (PgunType == 1 || PgunType == 2))
        {
            bulletScript.enabled = true;
        }
    }
}
