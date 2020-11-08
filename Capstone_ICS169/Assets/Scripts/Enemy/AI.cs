using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int type; // feel free to change these in inspector
    public bool isOn = true, willMove = true, willAttack = true, willTurn = true, willPatrol;
    public float duration;
    public Vector3 patrolEnd;
    public GameObject player, target; // don't change these and below variables
    public Transform position;
    public float randomTime, yVel, randomX, randomZ, currentDuration, speed;
    public Rigidbody rb;
    public float distance, cooldown, droneY;
    public int damage, random;
    public int[] damageArray = new int[4];
    public GameObject[] model = new GameObject[4];
    public GameObject bullet;
    public Vector3 patrolStart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //target = gameObject.transform.GetChild(0);
        //rb.velocity = new Vector3(0, -2, 0);
        damageArray[0] = 10; // set damage values for ai
        position = player.transform;
        model[type].SetActive(true); // set model and damage to chosen type
        damage = damageArray[type];
        this.GetComponent<MeshRenderer>().enabled = false;
        if (type == 0) // update collision to match model
        {
            this.gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0.125f, 0);
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.4f, 0.5f, 0.5f);
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
            patrolStart = transform.position;
            //if (patrolEnd == Vector3.zero) // set nearby random patrol point if none is set
                //patrolEnd = new Vector3(transform.position.x + Random.Range(-8f, 8f), transform.position.y, transform.position.z + Random.Range(-8, 8f));
            if (duration == 0)
                duration = Random.Range(2.5f, 5f);
            target.transform.position = patrolEnd;
            willAttack = false;
            willMove = false;
            willTurn = false;
            GetComponent<Rigidbody>().freezeRotation = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn) // enable AI
        {
            if (transform.position.y < 0) // fell off map
            {
                print(name + " has been deleted since it fell through ground.");
                Destroy(gameObject);
            }
            distance = Vector3.Distance(player.transform.position, this.transform.position); // check distance between this and player
            if (willTurn) // look at target
                transform.LookAt(player.transform.position);
            if (willMove) // move to target
            {
                //yVel = rb.velocity.y;
                //random = Random.Range(-1, 1);
                if (type == 0) // spider
                {
                    if (distance > 1.5f) // move closer if too far, else stops next to player, avoids humping
                        rb.velocity = transform.rotation * Vector3.forward * 5;
                    else
                        rb.velocity = Vector3.zero;
                    rb.velocity = new Vector3(rb.velocity.x, -1f, rb.velocity.z); // simulated gravity
                }
                else if (type == 1) // drone
                {
                    if (randomTime <= 0) // move to random position
                    {
                        randomTime = Random.Range(0.5f, 2f);
                        droneY = Random.Range(-2f, 2f);
                        if (this.transform.position.y < 2) // fly higher if too low
                            droneY = Random.Range(0, 2f);
                        if (this.transform.position.y > 6) // vice versa
                            droneY = Random.Range(-2f, 0);
                        if (distance < 8) // strafe if close by, else move closer
                            rb.velocity = new Vector3(Random.Range(-2f, 2f), droneY, Random.Range(-2f, 2f));
                        else
                            rb.velocity = transform.rotation * Vector3.forward * 3;
                    }
                    randomTime -= Time.deltaTime;
                }
                else if (type == 2) // racer (this is weird shit)
                {
                    if (randomTime <= 0) // move to random position
                    {
                        randomX = Random.Range(-1, 2);
                        randomZ = Random.Range(-1, 2);
                        randomTime = Random.Range(2f, 4f);
                    }
                    randomTime -= Time.deltaTime;
                    if (distance < 8) // strafe if close by, else move closer
                        rb.velocity = transform.rotation * new Vector3(randomX * 3, 0, randomZ * 3);
                    else
                        rb.velocity = transform.rotation * Vector3.forward * 3;
                    rb.velocity = new Vector3(rb.velocity.x, -1f, rb.velocity.z); // simulated gravity

                }
                //print(random);
                //print(rb.velocity);
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
            if (willPatrol) // patrol to a point continously
            {
                
                if (currentDuration <= 0)
                {
                    currentDuration = duration;
                    transform.localRotation *= Quaternion.Euler(0, 180, 0);
                    //transform.rotation = 
                }
                    
                rb.velocity = transform.rotation * Vector3.forward * 3; // move forward
                currentDuration -= Time.deltaTime;
                
                /*
                target.transform.position = patrolEnd;
                transform.LookAt(target.transform); // look at PatrolEnd
                rb.velocity = transform.rotation * Vector3.forward * 3; // move forward
                distance = Vector3.Distance(transform.position, target.transform.position); // check distance between this and patrolEnd
                if (distance <= 0.1f) // moves to opposite side
                {
                    transform.position = patrolEnd;
                    patrolEnd = patrolStart;
                    patrolStart = transform.position;
                }
                */
                
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
            GameObject bullet = Instantiate(this.bullet, transform.position + transform.rotation * Vector3.forward, this.transform.rotation);
        }
        else if (type == 2)
        {
            for (int i = 0; i < 3; ++i) // 3 shot burst
            {
                GameObject bullet = Instantiate(this.bullet, transform.position + transform.rotation * new Vector3(0, 0.45f, 1), this.transform.rotation);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
