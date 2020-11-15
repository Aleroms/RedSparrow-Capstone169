using UnityEngine;
/*
This script points the arm and gun twords the aim dot so we dont have to.
This is manyly for guns shooting physical bullets so the bullets travel towards the crosshair
Though this script can be attached to hitscan weapons for more consistent gun sway behavior
*/
public class AimHelper : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;//The speed at which the gun will move (in radians per sec)

    private Camera PlayerCamera;// This will be the origin point for the ray cast. Called it PlayerCamera cause I assume there's always gonna be 1 Camera attached to the player
    private Vector3 targetDir;
    private Vector3 newDirection;

    void Start()
    {
        PlayerCamera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = PlayerCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));//Origin of the ray is center of the screen
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, PlayerCamera.transform.forward, out hit, 1000) && hit.collider.tag != "Player") {//If we hit something with the ray cast and it's not the player
            targetDir = hit.point - transform.position;//Figure out the direction off the the target relative to the gun
            float singleStep = speed * Time.deltaTime;//Set the movement speed
            newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);//This calculates the new angles the gun needs to rotate in order to point at the target
            transform.rotation = Quaternion.LookRotation(newDirection);//Move the gun to point at the target
        }
        else if (Physics.Raycast(rayOrigin, PlayerCamera.transform.forward, out hit, 1000) && hit.collider.tag == "Player") {
            transform.rotation = Quaternion.Euler(Vector3.forward);
        }
    }
}
