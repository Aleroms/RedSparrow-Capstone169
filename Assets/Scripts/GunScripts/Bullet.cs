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
    private bool isMortar;
    //[SerializeField]
    //private GameObject bullet;
    public GameObject bulletPrefab;
    private Vector3 impactArea;
    private float spread = 3f;

    void Update()
    {
		transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            if (isMortar) // ai walker mortar attack
            {
                //GameObject player = GameObject.Find("Player");
                //transform.LookAt(player.transform);
                transform.LookAt(impactArea);
                for (int i = 0; i < 10; ++i) // shotgun
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.rotation * Vector3.forward, transform.rotation);
                    //bullet.transform.LookAt(impactArea);
                    bullet.GetComponent<Bullet>().SetDamage(6);
                    bullet.GetComponent<Bullet>().SetLifetime(5);
                    bullet.GetComponent<Bullet>().SetSpeed(25);
                    bullet.transform.Rotate(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
                    bullet.GetComponent<Bullet>().isMortar = false;
                    bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet")) // ignores collision with another bullet
        {
            if (collision.gameObject.CompareTag("Player"))
                collision.gameObject.GetComponent<Player>().Damage(bulletDamage / 2);
            AI health = collision.gameObject.GetComponent<AI>();
            if (health != null)
            {
                health.Damage(bulletDamage);
                GameObject player = player = GameObject.Find("Player");
                player.GetComponent<AccuracyCounter>().ShotsHit();
            }
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }

    public void SetDamage(int value)
    {
        bulletDamage = value;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public void SetLifetime(float value)
    {
        lifetime = value;
    }

    public void SetMortar(bool value)
    {
        isMortar = value;
    }

    public void SetImpact(Vector3 value)
    {
        impactArea = value;
    }
}
