using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    [SerializeField]
    private GameObject _bridge;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			_bridge.SetActive(true);
	}
}
