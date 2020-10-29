using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage = 1;
    [SerializeField]
    private float _speed = 5f;

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.gotDamaged(bulletDamage);
        }
        Destroy(gameObject);
    }
}
