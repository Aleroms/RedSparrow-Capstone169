using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int type; // feel free to change these in inspector
    public bool isOn = true, willMove = true, willAttack = true, willTurn = true, willPatrol;
    public float duration;
    public GameObject player, target, bullet; // don't change these and below variables
    public Transform position;
    public float randomTime, yVel, randomX, randomZ, currentDuration, speed;
    public Rigidbody rb;
    public float distance, cooldown, droneY, jumpCooldown, jumpDuration;
    public int damage, random;
    public int[] damageArray = new int[4];
    public GameObject[] model = new GameObject[4];
    public GameObject bulletPrefab;
    public bool canJump = true, fireFromRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // set player
        damageArray[0] = 10; // set damage values for ai
        position = player.transform;
        model[type].SetActive(true); // set model and damage to chosen type
        damage = damageArray[type];
        this.GetComponent<MeshRenderer>().enabled = false; // disable block placeholder
        if (type == 0) // update collision to match model and other specific type attributes
        {
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0.125f, 0);
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.4f, 0.5f, 0.5f);
            random = Random.Range(0, 2);
            if (random == 0)
                canJump = false;
        }

        else if (type == 1)
        {
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0.1f, 0);
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 0.25f, 0.5f);
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (type == 2)
        {
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0.35f, 0);
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.4f, 1.5f);
        }
        if (willPatrol)
        {
            target.SetActive(true);
            if (duration == 0)
                duration = Random.Range(2.5f, 5f);
            willAttack = false;
            willMove = false;
            willTurn = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            currentDuration = duration;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn) // enable AI
        {
            distance = Vector3.Distance(player.transform.position, this.transform.position); // check distance between this and player
            if (willTurn) // look at target
                if (type == 1) // only flier can tilt in y-axis
                    transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
                else
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (willMove) // move to target
            {
                if (type == 0) // spider
                {
                    speed = 5;
                    if (distance > 1.5f) // move closer if too far, else stops next to player
                        if (jumpDuration <= 0)
                            rb.velocity = transform.rotation * Vector3.forward * speed;
                        else
                            rb.velocity = transform.rotation * new Vector3(0, 0, 3) * speed;
                    else
                        rb.velocity = Vector3.zero;
                    if (canJump && distance > 4 && jumpCooldown <= 0)
                    {
                        yVel = transform.position.y;
                        jumpDuration = 0.5f;
                        jumpCooldown = Random.Range(3f, 6f);
                        print("leap attack");
                    }
                }
                else if (type == 1) // drone
                {
                    if (randomTime <= 0) // move to random position
                    {
                        speed = 2f;
                        randomTime = Random.Range(0.5f, 2f);
                        droneY = Random.Range(-speed, speed);
                        if (this.transform.position.y < 4) // fly higher if too low
                            droneY = Random.Range(0, speed);
                        else if (this.transform.position.y > 8) // vice versa
                            droneY = Random.Range(-speed, 0);
                        if (distance < 8) // strafe if close by, else move closer
                            rb.velocity = new Vector3(Random.Range(-speed, speed), droneY, Random.Range(-speed, speed));
                        else
                            rb.velocity = transform.rotation * Vector3.forward * speed;
                    }
                    randomTime -= Time.deltaTime;
                }
                else if (type == 2) // racer
                {
                    if (randomTime <= 0) // move to random position
                    {
                        speed = 3;
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
                        rb.velocity = transform.rotation * Vector3.forward * 3;
                }
            }
            if (willAttack && cooldown <= 0) // attack
            {
                if (type == 0 && distance <= 1.5) // spider melee
                {
                    cooldown = 2;
                    StartCoroutine(Attack());
                }
                else if (type == 1 && distance <= 8) // drone ranged
                {
                    cooldown = 1;
                    StartCoroutine(Attack());
                }
                else if (type == 2 && distance <= 8) // racer ranged
                {
                    cooldown = 3;
                    StartCoroutine(Attack());
                }
            }
            if (willPatrol) // move forward for duration seconds, then turns around
            {
                if (currentDuration <= 0) // when it reaches timer
                {
                    currentDuration = duration;
                    transform.localRotation *= Quaternion.Euler(0, 180, 0); // flip y-rotation by 180
                }
                rb.velocity = transform.rotation * Vector3.forward * 3; // move forward
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
        print("begin attack");
        if (type == 0)
        {
            yield return new WaitForSeconds(1); // delay before swing
            if (distance <= 1.5 && this.enabled == true) // damage if alive and in range
            {
                player.GetComponent<Player>().Damage(damage);
                print("hit");
            }
            else
                print("miss");
        }
        else if (type == 1)
        {
            bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * Vector3.forward, this.transform.rotation);
        }
        else if (type == 2)
        {
            for (int i = 0; i < 3; ++i) // 3 shot burst
            {
                if (!fireFromRight)
                    bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(0.2f, 0.9f, 1), this.transform.rotation);
                else
                    bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * new Vector3(-0.2f, 0.9f, 1), this.transform.rotation);
                fireFromRight = !fireFromRight;
                bullet.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
