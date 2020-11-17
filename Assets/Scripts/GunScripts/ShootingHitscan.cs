using System.Collections;
using UnityEngine;


// This script is for hitscan shooting using raycast
public class ShootingHitscan : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int type = 1; //What type of fire maode is used. 1 = semi auto, 2 = full auto
    [SerializeField]
    private int gunDamage = 1; //Damage the laser does
    [SerializeField]
    private float semifireRate = .25f; // How quickly the gun can fire as a semi-automatic
    [SerializeField]
    private float autofireRate = .25f; // How quickly the gun can fire as a fully automatic
    [SerializeField]
    private float weaponRange = 50f;// How far the laser can go
    [SerializeField]
    private float knockBack = 100f;// How hard the laser hits targets
    [SerializeField]
    private Transform gunEnd;// The end of the gun

    private Camera aimCamera;// This will be the origin point for the ray cast. 
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);// This is how long the lazer will last onscreen
    private LineRenderer laserLine;// This is the laser
    private float nextFireTime;//This used with fireRate determines when the gun can fire

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        aimCamera = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), player.GetComponent<PlayerKeyBindings>().getshootGun())) && Time.time > nextFireTime && type == 1)
        {
            setCoolDown();
            StartCoroutine(bulletEffect());
            Shoot();
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), player.GetComponent<PlayerKeyBindings>().getshootGun())) && Time.time > nextFireTime && type == 2) {
            setCoolDown();
            StartCoroutine(bulletEffect());
            Shoot();
        }
    }

    void Shoot()
    {
        player.GetComponent<AccuracyCounter>().ShotsFired();
        Vector3 rayOrigin = aimCamera.ViewportToWorldPoint(new Vector3(.5f,.5f,0f));//Origin of the ray is center of the screen
        RaycastHit hit;
        laserLine.SetPosition(0, gunEnd.position);//Origin of the laser is the end of the gun barrel
        if (Physics.Raycast(rayOrigin, aimCamera.transform.forward, out hit, weaponRange)) //If we hit something...
        {
            laserLine.SetPosition(1, hit.point);//Set the end of the laser to the thing we hit
            AI health = hit.collider.GetComponent<AI>();//Get the health of the thing we hit
            if (health != null) {//If there is health, call the enemy's damage function and hurt the enemy
                health.Damage(gunDamage);
                player.GetComponent<AccuracyCounter>().ShotsHit();
            }
            if (hit.rigidbody != null) {//If the enemy has a rigid body, push the enemy back 
                hit.rigidbody.AddForce(-hit.normal * knockBack);
            }
        }
        else {//If we hit nothing, set the end of the laser to it's max range
            laserLine.SetPosition(1, rayOrigin + (aimCamera.transform.forward * weaponRange));
        }
        gameObject.GetComponent<GunController>().decreaseAmmo();
    }

    void setCoolDown()
    {
        if (type == 1) {
            nextFireTime = Time.time + semifireRate;
        }
        else{
            nextFireTime = Time.time + autofireRate;
        }
    }

    private IEnumerator bulletEffect() {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
