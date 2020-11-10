using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSection : MonoBehaviour
{
    private CheckpointManager _chptManager;
	public string section;
	private void Start()
	{
		_chptManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
		if (_chptManager == null) Debug.LogError("could not find Checkpoint Manager gameobject in scene");
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			_chptManager.EnterSection(section);
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			_chptManager.ExitSection(section);
		}
	}
}
