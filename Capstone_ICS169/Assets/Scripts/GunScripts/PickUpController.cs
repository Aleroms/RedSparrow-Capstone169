using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField]
    private ShootingHitscan hitScanScript;
    [SerializeField]
    private ShootingPhysical bulletScript;
    [SerializeField]
    private Rigidbody gunRB;
    [SerializeField]
    private BoxCollider gunBC;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private float pickUpRange;
    [SerializeField]
    private float dropForwardForce;
    [SerializeField]
    private float dropUpwardForce;

    public bool isEquipped;
    public static bool slotFull;

    void Start()
    {
        if (!isEquipped) {
            setThingsFalse();
        }
        if (isEquipped) {
            setThingsTrue();
        }
    }

    void Update()
    {
        Vector3 distToPlayer = player.position - transform.position;
        if (!isEquipped && distToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) {
            PickUp();
        }
        if (isEquipped && Input.GetKeyDown(KeyCode.Q)){
            Drop();
        }
    }

    void PickUp() {
        setThingsTrue();
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

    void Drop() {
        setThingsFalse();
        transform.SetParent(null);
        gunRB.AddForce(playerCamera.forward * dropForwardForce, ForceMode.Impulse);
        gunRB.AddForce(playerCamera.forward * dropUpwardForce, ForceMode.Impulse);
    }

    void setThingsFalse() {
        isEquipped = false;
        slotFull = false;
        gunRB.isKinematic = false;
        gunBC.isTrigger = false;
        if (hitScanScript != null)
        {
            hitScanScript.enabled = false;
        }
        else if (bulletScript != null)
        {
            bulletScript.enabled = false;
        }
    }

    void setThingsTrue() {
        isEquipped = true;
        slotFull = true;
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
}
