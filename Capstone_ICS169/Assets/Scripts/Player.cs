using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private CharacterController _controller;

	[SerializeField]
	private float _speed = 5f;
	[SerializeField]
	private float _gravity = 9.82f;
	public int health = 100;
	public float cooldown;
	public Image healthbar;

	void Start()
	{
		_controller = GetComponent<CharacterController>();

		if (_controller == null) Debug.LogError("controller is null");

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		CalculateMovement();
		if (Input.GetKey(KeyCode.V) && cooldown == 0)
		{
			cooldown = 2;
			health -= 15;
			print("HP: " + (health + 15) + " -> " + health);
			if (health < 0)
			{
				print("Game Over");
				healthbar.enabled = false;
			}
			
			else
			{
				healthbar.transform.localScale = new Vector3(health / 100f, 1, 1);
				healthbar.transform.localPosition = new Vector2((1f - healthbar.transform.localScale.x) * -250f, healthbar.transform.localPosition.y);
			}
				

		}

		if (cooldown > 0)
			cooldown -= Time.deltaTime;
		if (cooldown <= 0)
			cooldown = 0;
	}
	
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
