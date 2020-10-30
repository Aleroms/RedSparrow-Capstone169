using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	private CharacterController _controller;

	[SerializeField]
	private bool _isgrounded;
	[SerializeField]
	private bool _iscrouching;

	[SerializeField]
	private float _speed = 5f;
	[SerializeField]
	private float _gravity = 9.82f;
	[SerializeField]
	private float _jumpHeight = 15f;
	[SerializeField]
	private float _crouchHeight = 0.8f;
	[SerializeField]
	private float _groundDistance = 0.4f;

	[SerializeField]
	private int _health = 100;

	[SerializeField]
	private Transform _groundCheck;
	[SerializeField]
	private Transform _ceilingCheck;

	[SerializeField]
	private LayerMask _groundMask;
	
	private Vector3 _velocity;

	void Start()
	{
		_controller = GetComponent<CharacterController>();

		if (_controller == null) Debug.LogError("controller is null");

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		Movement();
		Jump();
		Crouch();
	}
	
	void Movement()//function works properly when proper layerMask is set to ground. 
	{	//returns true if player is grounded
		_isgrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

		if(_isgrounded && _velocity.y < 0f)
			_velocity.y = -2f;
		

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 direction = x * transform.right + z * transform.forward;

		_controller.Move(direction * Time.deltaTime * _speed);
	
	}
	void Jump()
	{
		if (Input.GetButtonDown("Jump") && _isgrounded)
		{
			_velocity.y = Mathf.Sqrt(-2f * _jumpHeight * -1 * _gravity);
		}

		_velocity.y -= _gravity * Time.deltaTime;

		_controller.Move(_velocity * Time.deltaTime);
	}
	void Crouch()
	{	//works with ground layerMask
		_iscrouching = Physics.CheckSphere(_ceilingCheck.position, _groundDistance,_groundMask);

		if (Input.GetKey(KeyCode.LeftControl))
		{
			_iscrouching = true;
			_controller.height = 0.8f;
		}
		

		if(Input.GetKeyUp(KeyCode.LeftControl) && _iscrouching == false)
		{
			_controller.height = 1.8f;
		}
	
	}
	
	public int GetCurrentHealth()
	{
		return _health;
	}

	
}
