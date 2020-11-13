using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//sends player to credits menu
public class firstDeliverable : MonoBehaviour
{
    //basic way to end the game for our first
	//deliverable.
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
