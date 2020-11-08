using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
	//public Transform respawnPt;
	private CheckpointManager _checkPointManager;

	private void Start()
	{
		_checkPointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
		if (_checkPointManager == null) Debug.LogError("manager is null");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			_checkPointManager.DeadZone();
		}
	}
}
