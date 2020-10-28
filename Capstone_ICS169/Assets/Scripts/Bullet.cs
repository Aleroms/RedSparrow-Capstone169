using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float _speed = 5f;

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
