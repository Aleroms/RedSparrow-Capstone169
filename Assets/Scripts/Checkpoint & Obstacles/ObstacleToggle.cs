using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script tries to toggle an obstacle on/off
//current issue: you can still walk across 

public class ObstacleToggle : MonoBehaviour
{
	[SerializeField]
	private float _delta = 3f;
	[SerializeField]
	private float _offset = 1f;

	private MeshRenderer _mesh;
	private BoxCollider _boxCollider;
	
    // Start is called before the first frame update
    void Start()
    {
		_mesh = GetComponent<MeshRenderer>();
		_boxCollider = GetComponent<BoxCollider>();
		//does not work if 2nd box collider (the trigger) is above the 
		//collider with the box collider
	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			StartCoroutine(ToggleCoroutine());
		}
	}
	IEnumerator ToggleCoroutine()
	{
		yield return new WaitForSeconds(_delta);
		_mesh.enabled = false;
		_boxCollider.enabled = false;
		yield return new WaitForSeconds(_delta * _offset);
		_mesh.enabled = true;
		_boxCollider.enabled = true;

	}
}
