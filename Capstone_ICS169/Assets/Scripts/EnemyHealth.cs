using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the amount of enemy health and behavior once that reaches 0
public class EnemyHealth : MonoBehaviour
{
    public int HP = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player") {
            HP -= 1;
            Debug.Log("hit!");
            if (HP == 0)
            {
                Die();
            }
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
