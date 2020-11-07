using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
	[SerializeField]
	private List<GameObject> _sectionCheckpoint;

    private Transform _currentCheckpoint;
	private GameObject _previousCheckpoint;

	private GameObject _player;
	private CharacterController cc;
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

		//init for checkpoints
		if(_sectionCheckpoint != null)
		{
			_currentCheckpoint = _sectionCheckpoint[0].transform;
		}
		_previousCheckpoint = null;
		
	}
	private void TurnOffCheckpoint()
	{
		//"turns off" by changing color to red
		Renderer render = _previousCheckpoint.GetComponent<Renderer>();

		if(render != null)
		{
			Material mat = _previousCheckpoint.GetComponent<Renderer>().material;
			mat.color = new Color(1f, 0f, 0f);
		}
			
		
	}
	public void UpdateCheckpoint(Transform newCheckpoint)
	{
		//first checkpoint
		if(_previousCheckpoint != null)
			TurnOffCheckpoint();
		
		_previousCheckpoint = _currentCheckpoint.gameObject;

		_currentCheckpoint = newCheckpoint;
	}
	public void DeadZone()
	{
		//player has fallen and needs to respawn at current checkpoint
			

			if (cc != null)
				cc.enabled = false;

			_player.transform.position = _currentCheckpoint.position;
			StartCoroutine(CCEnableRoutine(cc));
		
		IEnumerator CCEnableRoutine(CharacterController cc)
		{
			yield return new WaitForSeconds(0.5f);

			if (cc != null)
				cc.enabled = true;
		}
	}
	public void SetSection(GameObject section)
	{	//avoid adding first section
		if(section.name != _sectionCheckpoint[0].name)
			_sectionCheckpoint.Add(section);
	}
}
