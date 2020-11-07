using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int type;
    public bool isOn = true, willMove = true, willAttack = true, willTurn = true;
    public GameObject player, target;
    public Transform position;
    public float randomTime;
    public Rigidbody rb;
    public float distance, cooldown, droneY;
    public int damage;
    public int[] damageArray = new int[4];
    public GameObject[] model = new GameObject[4];
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        damageArray[0] = 10; // set damage values for ai
        position = player.transform;
        model[type].SetActive(true); // set model and damage to chosen type
        damage = damageArray[type];
        this.GetComponent<MeshRenderer>().enabled = false;
        if (type == 0) // update collision to match model
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
        else if (type == 1)
            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
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
                transform.LookAt(position);
            if (willMove) // move to target
            {
                if (type == 0) // spider
                {
                    if (distance > 2) // move closer if too far, else stops next to player, avoids humping
                        rb.velocity = transform.rotation * Vector3.forward * 5;
                    else
                        rb.velocity = Vector3.zero;
                    rb.velocity = new Vector3(rb.velocity.x, -2, rb.velocity.z); // stops the motherfucker from floating by dragging his ass downwards (band-aid)
                }
                else if (type == 1) // drone
                {
                    if (randomTime <= 0) // move to random position
                    {
                        randomTime = Random.Range(0.5f, 2f);
                        //print(randomTime);
                        droneY = Random.Range(-2f, 2f);
                        if (this.transform.position.y < 2) // fly higher if too low
                            droneY = Random.Range(0, 2f);
                        if (this.transform.position.y > 6) // vice versa
                            droneY = Random.Range(-2f, 0);
                        if (distance < 8) // strafe if close by, else move closer
                            rb.velocity = new Vector3(Random.Range(-2f, 2f), droneY, Random.Range(-2f, 2f));
                        else
                            rb.velocity = transform.rotation * Vector3.forward * 3;
                        //rb.velocity = new Vector3(rb.velocity.x, -1, rb.velocity.z);
                        //Vector3 movement = Player.transform.rotation * Vector3.forward
                    }
                    randomTime -= Time.deltaTime;
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
            }
        }
        else // disable AI
        {
            willMove = false;
            willAttack = false;
            willTurn = false;
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            //if (cooldown <= 0)
                //cooldown = 0;
        }

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
    }
}
