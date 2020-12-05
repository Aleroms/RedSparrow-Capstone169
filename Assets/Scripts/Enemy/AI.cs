using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enemy Types: 0 = spider, 1 = drone, 2 = racer, 3 = walker
public class AI : MonoBehaviour
{
    public int type;
    public bool isOn = true, willMove = true, willAttack = true, willTurn = true, willPatrol;
    public float duration;
    public GameObject bulletPrefab;
    private GameObject player, bullet;
    private float randomTime, randomX, randomZ, currentDuration, speed, distance, cooldown, jumpCooldown, jumpDuration, attackCooldown, gravity = -9.81f, x, y, z;
    private int damage, random, health, nearThreshold = 6, farThreshold = 12;
    private bool canJump = true, fireFromRight, canSeePlayer;
    private Vector3 formerRotation, direction;
    private CharacterController controller;
    public string[] nameArray = new string[4];
    public Mesh[] meshArray = new Mesh[4];
    public int[] healthArray = new int[4];
    public int[] damageArray = new int[4];
    public float[] attackCooldownArray = new float[4];
    public float[] speedArray = new float[4];
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);
    private GameObject end, losChecker;
    private RaycastHit hit;
    private Vector3 rayOrigin;
    // private int layerMask = 1 << 11;
    private LayerMask mask; // = LayerMask.GetMask("Checkpoints");
    [SerializeField]
    private Vector3 targetPosition;
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
        laserLine = GetComponent<LineRenderer>();
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
        else if (type == 1)
        {

        }
            
        else if (type == 2)
            controller.height = 1.2f;
        else if (type == 3)
        {
            controller.radius = 1f;
            controller.height = 4f;
            controller.center = new Vector3(0, 0, -1);
            end = gameObject.transform.GetChild(1).gameObject;
            end.SetActive(true);
        }
        losChecker = gameObject.transform.GetChild(0).gameObject;
        losChecker.transform.localPosition = new Vector3(0, controller.height / 3, 0); // elevate los to roughly head height
        //mask = LayerMask.GetMask("Checkpoints");
        //mask = ~mask;

    }
    void Update()
    {
        if (targetPosition != Vector3.zero)
            canSeePlayer = true;
        else
            canSeePlayer = false;
        distance = Vector3.Distance(player.transform.position, this.transform.position); // check distance between this and player
        losChecker.transform.LookAt(player.transform); // check if enemy has line-of-sight of player
        //if (distance <= nearThreshold) // can act regardless if close enough to player
            //canSeePlayer = true;
        //else
        //{
            //int layerMask = 1 << 11;
            //layerMask = ~layerMask;
            if (Physics.Raycast(losChecker.transform.position, losChecker.transform.forward, out hit)) //layerMask
            {
                //print(hit.transform.gameObject.name);
                if (hit.transform.root.gameObject == player)
                    //canSeePlayer = true;
                    targetPosition = player.transform.position;
                //else
                    //canSeePlayer = false;
            }
        //}
        //print(canSeePlayer);
        if (type != 1) // gravity
        {
            if (!controller.isGrounded) // acceleration when midair
            {
                y += gravity * Time.deltaTime;
                direction = x * transform.right + y * transform.up + z * transform.forward;
                controller.Move(direction * speed * Time.deltaTime);
            }
            else
                y = 0;
        }
        if (isOn) // enable AI
        {
            name = nameArray[type] + " (" + (int)transform.position.x + ", " + (int)transform.position.y + ", " + (int)transform.position.z + ")"; // update name to show world position
            if (willTurn && canSeePlayer) // look at player
            {
                formerRotation = transform.rotation.eulerAngles;
                if (type == 1) // only flier can tilt in y-axis
                    transform.LookAt(new Vector3(targetPosition.x, targetPosition.y, targetPosition.z));
                else
                    transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
            }
            if (willMove && canSeePlayer) // move to target
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
                            randomTime = 3;
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
                else if (type == 3) // walker
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
            if (willAttack && cooldown <= 0 && canSeePlayer) // attack
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
        cooldown = Random.Range(attackCooldown, attackCooldown * 1.5f);
        if (type == 0)
        {
            yield return new WaitForSeconds(0.5f); // delay before swing
            if (distance <= 1.5 && enabled == true) // damage if alive and in range
                player.GetComponent<Player>().Damage(damage);
        }
        else if (type == 1)
        {
            rayOrigin = transform.position;
            laserLine.SetPosition(0, rayOrigin);
            //transform.LookAt(player.transform);
            laserLine.SetPosition(1, rayOrigin + (transform.forward * 50));
            controller.Move(Vector3.zero); // while aiming, remove ability to turn and move
            laserLine.enabled = true;
            willTurn = false;
            willMove = false;
            yield return shotDuration; // damages after delay
            laserLine.enabled = false;
            willTurn = true;
            willMove = true;
            randomTime = 0;
            if (Physics.Raycast(rayOrigin, transform.forward, out hit, 50)) // collision check
            {
                //if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Enemy"))
                Player health = hit.collider.GetComponent<Player>(); // Damage player
                if (health != null)
                    health.Damage(damage);
            }
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
                bullet.transform.LookAt(new Vector3(targetPosition.x, targetPosition.y, targetPosition.z));
                bullet.GetComponent<Bullet>().SetDamage(damage);
                yield return new WaitForSeconds(0.33f);
            }
            if (Random.Range(0, 2) == 1) // randomizes where the first shot comes from
                fireFromRight = !fireFromRight;
        }
        else if (type == 3)
        {
            bullet = Instantiate(bulletPrefab, end.transform.position + transform.rotation * Vector3.forward, transform.rotation);
            bullet.GetComponent<Bullet>().SetDamage(damage);
            bullet.GetComponent<Bullet>().SetSpeed(6);
            bullet.GetComponent<Bullet>().SetLifetime((distance / 2) / 6);
            bullet.GetComponent<Bullet>().SetMortar(true);
            bullet.GetComponent<Bullet>().SetImpact(targetPosition);
            bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
    public void Damage(int value)
    {
        health -= value;
        if (health <= 0)
            Destroy(gameObject);
    }
}