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
    private GameObject player;
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
        Vector3 distToPlayer = player.transform.position - transform.position;
        if (!isEquipped && distToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getPickUp()) && !slotFull) {
            PickUp();
        }
        if (isEquipped && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getDrop()))
        {
            Drop();
        }
        if(isEquipped && Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getSwitchFireType())){
            SwitchFireType();
        }
    }

    void PickUp() {
        gameObject.tag = "Player";
        setThingsTrue();
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

    void Drop() {
        gameObject.tag = "Untagged";
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

    void SwitchFireType() {
        if (hitScanScript != null && bulletScript != null) {
            if (hitScanScript.enabled == true) {
                hitScanScript.enabled = false;
                bulletScript.enabled = true;
            }
            else if (bulletScript.enabled == true) {
                hitScanScript.enabled = true;
                bulletScript.enabled = false;
            }
        }
    }
}
