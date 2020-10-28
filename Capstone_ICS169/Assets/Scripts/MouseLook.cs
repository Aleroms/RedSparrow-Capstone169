using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	[SerializeField]
	private float _sensitivity = 100f;
	[SerializeField]
	private Transform _player;
	private float _xRot;
	

	// Update is called once per frame
	void Update()
	{
		float _mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
		float _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

		_xRot -= _mouseY;
		_xRot = Mathf.Clamp(_xRot, -90, 90);

		transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
		_player.Rotate(Vector3.up * _mouseX);
	}
}
