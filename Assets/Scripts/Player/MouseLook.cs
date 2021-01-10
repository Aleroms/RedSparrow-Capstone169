using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
	[SerializeField]
	private float _sensitivity;
	private float _xRot;

	[SerializeField]
	private Transform _player;

	[SerializeField]
	private bool _isWebGLBuild = true;

	PlayerStatTrack statTrack;
	public Text debugText;

    private void Start()
    {
        _player = transform.parent.transform;
		statTrack = _player.GetComponent<PlayerStatTrack>();

		if (statTrack == null) Debug.LogError("playerstatstrack is null");


		Cursor.lockState = CursorLockMode.Locked;
	}
    // Update is called once per frame
    void Update()
	{
		//player.GetComponent<PlayerStatTrack>().setMouseSensitivity(3.2f);
		//float sensitivity = _player.GetComponent<PlayerStatTrack>().getMouseSensitivity();
		_sensitivity = statTrack.getMouseSensitivity();

		if(debugText != null)
			debugText.text = "Mouse Sensitivity:" + _sensitivity.ToString("F3");

		print(_sensitivity);
		float _mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
		float _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

		if(_isWebGLBuild)
		{
			_mouseX = Mathf.Clamp(_mouseX, -1, 1);
			_mouseY = Mathf.Clamp(_mouseY, -1, 1);
		}
		

		_xRot -= _mouseY;
		_xRot = Mathf.Clamp(_xRot, -90, 90);

		transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
		_player.Rotate(Vector3.up * _mouseX);
	}
}
