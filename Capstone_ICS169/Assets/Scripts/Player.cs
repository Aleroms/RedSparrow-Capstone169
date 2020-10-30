using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	private CharacterController _controller;

	public bool _isgrounded;

	[SerializeField]
	private float _speed = 5f;
	[SerializeField]
	private float _gravity = 9.82f;
	[SerializeField]
	private float _jumpHeight = 15f;
	private float _yVelocity;

	[SerializeField]
	private int health = 100;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	
	Vector3 velocity;

	void Start()
	{
		_controller = GetComponent<CharacterController>();

		if (_controller == null) Debug.LogError("controller is null");

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		CalculateMovement();
		
	}
	
	void CalculateMovement()
	{
		_isgrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if(_isgrounded && velocity.y < 0f)
		{
			velocity.y = -2f;
		}
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 direction = x * transform.right + z * transform.forward;

		_controller.Move(direction * Time.deltaTime * _speed);

		if(Input.GetButtonDown("Jump") && _isgrounded)
		{
			velocity.y = Mathf.Sqrt(-2f * _jumpHeight * -1 *_gravity);
		}

		velocity.y -= _gravity * Time.deltaTime;

		_controller.Move(velocity * Time.deltaTime);
		
	}
	void OnTriggerEnter()
	{
		_isgrounded = true;//jumping
	}
	public int GetCurrentHealth()
	{
		return health;
	}

	
}
