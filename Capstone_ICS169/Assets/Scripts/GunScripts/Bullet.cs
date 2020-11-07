using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage = 1;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float lifetime = 2f;

    void Update()
    {
		transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().Damage(bulletDamage * 10);
        EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.gotDamaged(bulletDamage);
        }
        Die();
    }

    private void Die() {
        Destroy(gameObject);
    }
}
