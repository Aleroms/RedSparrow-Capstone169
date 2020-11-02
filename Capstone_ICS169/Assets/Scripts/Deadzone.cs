﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
	public Transform respawnPt;
    
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			CharacterController cc = other.GetComponent<CharacterController>();

			if (cc != null)
				cc.enabled = false;

			other.transform.position = respawnPt.position;
			StartCoroutine(CCEnableRoutine(cc));
		}
		IEnumerator CCEnableRoutine(CharacterController cc)
		{
			yield return new WaitForSeconds(0.25f);

			if (cc != null)
				cc.enabled = true;
		}
	}
}
