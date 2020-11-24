using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
	[SerializeField]//code breaks when not serialized
	private List<GameObject> _sectionCheckpoint;
	//[SerializeField]
	//private Stack<GameObject> _sectionCheckpoint;
	private int _sectionIndex = 0;
    private Transform _currentCheckpoint;
	private GameObject _previousCheckpoint;

	private GameObject _player;
	private CharacterController cc;
	/*
	[SerializeField]
	private bool _sectionA;
	[SerializeField]
	private bool _sectionB;
	[SerializeField]
	private bool _sectionC;*/
	//private Checkpoint _checkpointScript;
	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		
		if (_player != null)
		{
			cc = _player.GetComponent<CharacterController>();
			if (cc == null) Debug.LogError("characterController is null");
		}
		else
		{
			Debug.LogError("_player is null");
		}
		/*
		//init for checkpoints
		if(_sectionCheckpoint != null)
		{
			_currentCheckpoint = _sectionCheckpoint[0].transform;
			_previousCheckpoint = _sectionCheckpoint[0];
		}
		//_previousCheckpoint = null;
		*/
	}
	private void TurnOffCheckpoint()
	{
		//"turns off" by changing color to red
		//Renderer render = _previousCheckpoint.GetComponent<Renderer>();
		MeshRenderer render = _previousCheckpoint.GetComponent<MeshRenderer>();

		if (render != null)
		{
			render.material.color = new Color(1f, 0f, 0f);
			//Material mat = _previousCheckpoint.GetComponent<Renderer>().material;
			//mat.color = new Color(1f, 0f, 0f);
		}
		else
			Debug.LogError("render is null");
			
		
	}
	public void UpdateCheckpoint(Transform newCheckpoint)
	{
		/*//first checkpoint
		if(_secondCheckpointVisited)
			TurnOffCheckpoint();
		_previousCheckpoint = _currentCheckpoint.gameObject;
		*/
		_currentCheckpoint = newCheckpoint;

		Checkpoint prev;

		if(_previousCheckpoint != null)
		{
			prev = _previousCheckpoint.GetComponent<Checkpoint>();
			prev.DeactivateCheckpoint();
			_previousCheckpoint = _currentCheckpoint.gameObject;
		}
		else
		{
			_previousCheckpoint = _currentCheckpoint.gameObject;
			print("null probably a section");
		}

		//if(_currentCheckpoint != null)
		//_previousCheckpoint = _currentCheckpoint.gameObject;

		

		//print(_previousCheckpoint.name +_previousCheckpoint.transform.position);
		//print(newCheckpoint.name + newCheckpoint.position);
		//print(_currentCheckpoint.name + _currentCheckpoint.position);
		//_secondCheckpointVisited = true;
	}
	public void EnterSection(string sectionName, GameObject sectionCheckpoint)
	{
		/*
		if (sectionName == "Section A")
			_sectionA = true;
		else if (sectionName == "Section B")
			_sectionB = true;
		else if (sectionName == "Section C")
			_sectionC = true;
		*/
		if(sectionCheckpoint != null)
		{
			print("section added" + sectionCheckpoint.name);
			_sectionCheckpoint.Add(sectionCheckpoint);
		}
	}
	public void ExitSection(string sectionName)
	{
		/*
		if (sectionName == "Section A")
			_sectionA = false;
		else if (sectionName == "Section B")
			_sectionB = false;
		else if (sectionName == "Section C")
			_sectionC = false;
		/*
		_sectionIndex++;
		_currentCheckpoint = _sectionCheckpoint[_sectionIndex].transform;
		_previousCheckpoint = _currentCheckpoint.gameObject;
		//idk what i wanna do here
		*/
	}
	public void DeadZone()
	{
		//player has fallen and needs to respawn at current checkpoint
			

			if (cc != null)
				cc.enabled = false;

		if (_currentCheckpoint == null)
		{
			//trying to get the last checkpoint added
			int index = _sectionCheckpoint.Count - 1;
			print(index);
			_player.transform.position = _sectionCheckpoint[index].transform.position;
		}
		else
			_player.transform.position = _currentCheckpoint.position;
		

		StartCoroutine(CCEnableRoutine(cc));
		
		IEnumerator CCEnableRoutine(CharacterController cc)
		{
			yield return new WaitForSeconds(0.5f);

			if (cc != null)
				cc.enabled = true;
		}
	}
	
}
