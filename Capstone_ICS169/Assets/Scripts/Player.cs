﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private CharacterController _controller;

	[SerializeField]
	private float _speed = 5f;
	[SerializeField]
	private float _gravity = 9.82f;

	/*[SerializeField]
	private GameObject _bulletPrefab;*/

	void Start()
	{
		_controller = GetComponent<CharacterController>();

		if (_controller == null) Debug.LogError("controller is null");

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		CalculateMovement();

		/*if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			Shoot();
		}*/
	}

	/*void Shoot()
	{
		Debug.Log("testing");
		Vector3 offset = new Vector3(0, 0.25f, 1);
		GameObject bullet = Instantiate(_bulletPrefab, transform.position + offset, Quaternion.identity);
		
		Destroy(bullet, 4.0f);
	}*/

	void CalculateMovement()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(x, 0, z);
		Vector3 velocity = direction * _speed;
		velocity.y -= _gravity;

		velocity = transform.TransformDirection(velocity);
		_controller.Move(velocity * Time.deltaTime * _speed);
	}
}
