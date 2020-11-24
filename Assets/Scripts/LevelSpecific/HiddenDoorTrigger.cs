using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoorTrigger : MonoBehaviour
{
	[SerializeField]
	private GameObject _HiddenDoor;
	[SerializeField]
	private GameObject _spawnEnemy;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_HiddenDoor.SetActive(false);
			_spawnEnemy.SetActive(true);
		}
	}
}
