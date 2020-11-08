using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //check if player has reached checkpoint
    private bool _trigger = false;
	private CheckpointManager _checkPointManager;

	[SerializeField]
	private bool _SectionCheckpoint;

	private void Start()
	{
		_checkPointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
		if (_checkPointManager == null) Debug.LogError("manager is null");
	}
	private void OnTriggerEnter(Collider other)
	{
		//if player enters trigger again then it should not trigger update
		if(_trigger == false)
		{
			if (other.tag == "Player")
			{
				//signal to the player they have landed on checkpoint
				Material mat = this.GetComponent<Renderer>().material;
				mat.color = new Color(0f, 1f, 0f);

				//notify gameManager this is current checkpoint;
				_checkPointManager.UpdateCheckpoint(transform);
				_trigger = true;
			}
			/*
			if(_SectionCheckpoint == true)
			{
				_checkPointManager.SetSection(gameObject);
			}
			*/
		}
		
	}
	
}
