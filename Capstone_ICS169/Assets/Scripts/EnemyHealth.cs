using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the amount of enemy health and behavior once that reaches 0
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int HP = 10;

    public void gotDamaged(int damage) {
        Debug.Log("hit!");
        HP -= damage;
        if (HP == 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
