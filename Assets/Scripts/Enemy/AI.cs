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
    private float randomTime, randomX, randomZ, currentDuration, speed, distance, cooldown, droneY, jumpCooldown, jumpDuration, attackCooldown;
    private Rigidbody rb;
    private int damage, random, health;
    private bool canJump = true, fireFromRight;
    private Vector3 formerRotation;
    public string[] nameArray = new string[4];
    public Mesh[] meshArray = new Mesh[4];
    public int[] healthArray = new int[4];
    public int[] damageArray = new int[4];
    public float[] attackCooldownArray = new float[4];
    public float[] speedArray = new float[4];

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // set up enemy values
        name = nameArray[type];
        GetComponent<MeshCollider>().sharedMesh = meshArray[type];
        GetComponent<MeshFilter>().sharedMesh = meshArray[type];
        health = healthArray[type];
        damage = damageArray[type];
        attackCooldown = attackCooldownArray[type];
        speed = speedArray[type];
        player = GameObject.Find("Player");
        if (type == 1) // remove gravity from drone
            GetComponent<Rigidbody>().useGravity = false;
        if (willPatrol) // special patrol state, disables standard movement
        {
            if (duration == 0)
                duration = Random.Range(2.5f, 5f);
            willMove = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            currentDuration = duration;
        }
    }
    void Update()
    {
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
                        if (jumpDuration <= 0)
                            rb.velocity = transform.rotation * Vector3.forward * speed;
                        else
                            rb.velocity = transform.rotation * new Vector3(0, 0, 3) * speed; // dash speed triples base speed
                    else
                        rb.velocity = Vector3.zero;
                    if (canJump && distance > 4 && jumpCooldown <= 0) // dash attack when off cooldown
                    {
                        jumpDuration = 0.5f;
                        jumpCooldown = Random.Range(3f, 6f);
                    }
                }
                else if (type == 1) // drone
                {
                    if (randomTime <= 0) // move to random position periodically
                    {
                        randomTime = Random.Range(0.5f, 2f); // change direction every X seconds
                        droneY = Random.Range(-speed, speed);
                        if (this.transform.position.y < 4) // fly higher if too low
                            droneY = Random.Range(0, speed);
                        else if (this.transform.position.y > 8) // vice versa
                            droneY = Random.Range(-speed, 0);
                        if (distance < 8) // strafe if close by, else move closer
                            rb.velocity = new Vector3(Random.Range(-speed, speed), droneY, Random.Range(-speed, speed)); // speed is randomized
                        else
                            rb.velocity = transform.rotation * Vector3.forward * speed;
                    }
                    randomTime -= Time.deltaTime;
                }
                else if (type == 2) // racer
                {
                    speed = speedArray[type];
                    if (randomTime <= 0) // move to random position periodically
                    {
                        randomX = Random.Range(-1, 2);
                        randomZ = Random.Range(-1, 2);
                        randomTime = Random.Range(2f, 4f);
                        if (randomX != 0 && randomZ != 0)
                            speed = Mathf.Sqrt(Mathf.Pow(speed, 2) / 2);
                    }
                    randomTime -= Time.deltaTime;
                    if (distance < 8) // strafe if close by, else move closer
                        rb.velocity = transform.rotation * new Vector3(randomX * speed, 0, randomZ * speed);
                    else
                        rb.velocity = transform.rotation * Vector3.forward * speed;
                }
            }
            if (willAttack && cooldown <= 0) // attack
            {
                if ((type == 0 && distance <= 1.5) || (type == 1 && distance <= 8) || (type == 2 && distance <= 8)) // matching attack conditions
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
                rb.velocity = transform.rotation * Vector3.forward * speed; // move forward
                currentDuration -= Time.deltaTime;
            }
        }
        else // disable AI
        {
            willMove = false;
            willAttack = false;
            willTurn = false;
            rb.velocity = Vector3.zero;
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
            yield return new WaitForSeconds(1); // delay before swing
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
            for (int i = 0; i < 3; ++i) // 3 shot burst
            {
                if (!fireFromRight) // alternate shots from each hand
                    bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(0.2f, 0.9f, 1), transform.rotation);
                else
                    bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(-0.2f, 0.9f, 1), transform.rotation);
                fireFromRight = !fireFromRight;
                bullet.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
                bullet.GetComponent<Bullet>().SetDamage(damage);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public void Damage(int value)
    {
        health -= value;
        if (health <= 0)
            Destroy(gameObject);
    }
}
