using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for shooting the physical prototype building
//Basically moved shoot function from the Player script into here
public class ShootingPhysical : MonoBehaviour
{ 
    [SerializeField]
    private GameObject _bulletPrefab; 

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("testing");
        Vector3 offset = new Vector3(0, 0.25f, 1);
        GameObject bullet = Instantiate(_bulletPrefab, transform.position + offset, transform.rotation);

        Destroy(bullet, 4.0f);
    }
}
