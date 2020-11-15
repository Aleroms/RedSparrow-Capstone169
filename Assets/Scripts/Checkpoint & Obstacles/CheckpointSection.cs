using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSection : MonoBehaviour
{
	[SerializeField]
	private GameObject[] _checkpoints;
		
    private CheckpointManager _chptManager;
	//private CheckpointSection _chptSection;
	public string section;
	private void Start()
	{
		_chptManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
		
		if (_chptManager == null) Debug.LogError("could not find Checkpoint Manager gameobject in scene");
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
			_chptManager.EnterSection(section,gameObject);
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			_chptManager.ExitSection(section);
		}
	}
}
