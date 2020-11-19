using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int type;
    public bool isOn = true, willMove = true, willAttack = true, willTurn = true, willPatrol;
    public float duration;
    public GameObject bulletPrefab;
    private GameObject player, bullet;
    private float randomTime, randomX, randomZ, currentDuration, speed, distance, cooldown, jumpCooldown, jumpDuration, attackCooldown, gravity = -9.81f, x, y, z;
    private int damage, random, health, nearThreshold = 6, farThreshold = 12;
    private bool canJump = true, fireFromRight;
    private Vector3 formerRotation, direction;
    private CharacterController controller;
    public string[] nameArray = new string[4];
    public Mesh[] meshArray = new Mesh[4];
    public int[] healthArray = new int[4];
    public int[] damageArray = new int[4];
    public float[] attackCooldownArray = new float[4];
    public float[] speedArray = new float[4];
    private LineRenderer laserLine;// This is the laser
    private WaitForSeconds shotDuration = new WaitForSeconds(1f); //.07
    private GameObject end;
    private RaycastHit hit;
    private Vector3 rayOrigin;
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        name = nameArray[type];
        GetComponent<MeshCollider>().sharedMesh = meshArray[type];
        GetComponent<MeshFilter>().sharedMesh = meshArray[type];
        health = healthArray[type];
        damage = damageArray[type];
        attackCooldown = attackCooldownArray[type];
        speed = speedArray[type];
        player = GameObject.Find("Player");
        if (willPatrol) // special patrol state, disables standard movement
        {
            if (duration == 0)
                duration = Random.Range(2.5f, 5f);
            willMove = false;
            currentDuration = duration;
        }
        if (type == 0) // transform capsule collider size to match type
        {
            controller.radius = 0.25f;
            controller.height = 0.5f;
        }
        else if (type == 2)
            controller.height = 1.2f;
        else if (type == 3)
        {
            controller.radius = 1f;
            controller.height = 4f;
            
            end = this.gameObject.transform.GetChild(0).gameObject;
            end.SetActive(true);
            laserLine = GetComponent<LineRenderer>();
        }
    }
    void Update()
    {
        if (type != 1) // gravity
        {
            if (!controller.isGrounded) // acceleration when midair
                y += gravity * Time.deltaTime;
            else
                y = 0;
            direction = x * transform.right + y * transform.up + z * transform.forward;
            controller.Move(direction * speed * Time.deltaTime);
        }
        if (isOn) // enable AI
        {
            name = nameArray[type] + " (" + (int)transform.position.x + ", " + (int)transform.position.y + ", " + (int)transform.position.z + ")"; // update name to show world position
            distance = Vector3.Distance(player.transform.position, this.transform.position); // check distance between this and player
            if (willTurn) // look at player
            {
                formerRotation = transform.rotation.eulerAngles;
                if (type == 1) // only flier can tilt in y-axis
                    transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
                else
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }
            if (willMove) // move to target
            {
                if (type == 0) // spider
                {
                    if (distance > 1.5f) // move closer if too far, else stops next to player
                        if (jumpDuration <= 0) // standard move
                            z = 1;
                        else // leap bonus
                        {
                            z = 3;
                            y = 0.5f;
                        }
                    else
                        z = 0;
                    if (canJump && distance > nearThreshold && jumpCooldown <= 0) // leap when off cooldown
                    {
                        jumpDuration = 0.5f;
                        jumpCooldown = Random.Range(3f, 6f);
                    }
                }
                else if (type == 1 || type == 2) // drone or racer
                {
                    if (randomTime <= 0) // move to random position periodically
                    {
                        randomTime = Random.Range(type * 1.0f, type * 2.0f);
                        x = Random.Range(-1, 2);
                        if (distance < nearThreshold) // move away from player
                            z = -1;
                        else if (distance > farThreshold) // move towards player
                            z = 1;
                        else // strafe in any 8 way direction
                            z = Random.Range(-1, 2);
                        if (type == 1) // drone height direction
                        {
                            if (transform.position.y < nearThreshold / 2) // move away from ground
                                y = 1;
                            else if (transform.position.y > farThreshold / 2) // move towards ground
                                y = -1;
                            else // strafe in any y direction
                                y = Random.Range(-1, 2);
                        }
                    }
                    randomTime -= Time.deltaTime;
                }
                else if (type == 3) // drone or racer
                {
                    if (randomTime <= 0) // move to random position periodically
                    {
                        randomTime = 3;
                        x = Random.Range(-1, 2);
                        if (distance < nearThreshold * 2) // move away from player
                            z = -1;
                        else if (distance > farThreshold * 2) // move towards player
                            z = 1;
                        else // strafe in any 8 way direction
                            z = Random.Range(-1, 2);
                    }
                    randomTime -= Time.deltaTime;
                }
                direction = x * transform.right + y * transform.up + z * transform.forward;
                controller.Move(direction * speed * Time.deltaTime);
            }
            if (willAttack && cooldown <= 0) // attack
            {
                if ((type == 0 && distance <= 1.5f) || ((type == 1 || type == 2) && distance <= farThreshold) || (type == 3 && distance <= farThreshold * 3)) // matching attack conditions
                    StartCoroutine(Attack());
            }
            if (willPatrol) // move forward for duration seconds, then turns around
            {
                if (willTurn)
                    transform.localRotation = Quaternion.Euler(formerRotation); // set facing back to patrol path
                if (currentDuration <= 0) // when it reaches timer
                {
                    currentDuration = duration;
                    transform.localRotation *= Quaternion.Euler(0, 180, 0); // flip y-rotation by 180
                }
                currentDuration -= Time.deltaTime;
                direction = transform.forward;
                controller.Move(direction * speed * Time.deltaTime);
            }
        }
        else // disable AI
        {
            willMove = false;
            willAttack = false;
            willTurn = false;
        }
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        if (jumpCooldown > 0)
            jumpCooldown -= Time.deltaTime;
        if (jumpDuration > 0)
            jumpDuration -= Time.deltaTime;
    }
    IEnumerator Attack()
    {
        cooldown = attackCooldown;
        if (type == 0)
        {
            yield return new WaitForSeconds(0.5f); // delay before swing
            if (distance <= 1.5 && enabled == true) // damage if alive and in range
                player.GetComponent<Player>().Damage(damage);
        }
        else if (type == 1)
        {
            bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * Vector3.forward, transform.rotation);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
        else if (type == 2)
        {
            for (int i = 0; i < 4; ++i) // 4 shot burst over 1 second
            {
                if (!fireFromRight) // alternate shots from each hand
                    bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(0.2f, 0.5f, 1), transform.rotation);
                else
                    bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(-0.2f, 0.5f, 1), transform.rotation);
                fireFromRight = !fireFromRight;
                bullet.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
                bullet.GetComponent<Bullet>().SetDamage(damage);
                yield return new WaitForSeconds(0.33f);
            }
            if (Random.Range(0, 2) == 1) // randomizes where the first shot comes from
                fireFromRight = !fireFromRight;
        }
        else if (type == 3)
        {
            StartCoroutine(bulletEffect());
            rayOrigin = end.transform.position; // + transform.rotation * new Vector3(0, 2f, 0); // + transform.rotation * new Vector3(0, 2f, 0); // aimCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));//Origin of the ray is center of the screen
            
            laserLine.SetPosition(0, rayOrigin);//Origin of the laser is the end of the gun barrel
            end.transform.LookAt(player.transform);
            laserLine.SetPosition(1, rayOrigin + (end.transform.forward * 50));

        }
    }
    public void Damage(int value)
    {
        health -= value;
        if (health <= 0)
            Destroy(gameObject);
    }
    private IEnumerator bulletEffect()
    {
        x = 0;
        z = 0;
        controller.Move(Vector3.zero); // while aiming, remove ability to turn and move
        laserLine.enabled = true;
        willTurn = false;
        willMove = false;
        yield return shotDuration;
        laserLine.enabled = false;
        willTurn = true;
        willMove = true;
        randomTime = 0;
        if (Physics.Raycast(rayOrigin, end.transform.forward, out hit, 50)) //If we hit something...aimCamera.
        {
            //print("hit");
            //laserLine.SetPosition(1, hit.point);//Set the end of the laser to the thing we hit
            Player health = hit.collider.GetComponent<Player>();//Get the health of the thing we hit
            if (health != null)
                health.Damage(damage);

        }
        else
        {
            //print("miss");
            //laserLine.SetPosition(1, rayOrigin + (end.transform.forward * 20)); //transform.rotation.eulerAngles * 1
        }
    }
}