using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPrompt : MonoBehaviour
{
    [SerializeField]
    private string popUpPrompt;

	private PauseMenuUI _pause;
	private bool __hasTriggered = false;

	private void Start()
	{
		_pause = GameObject.Find("Canvas").GetComponent<PauseMenuUI>();
		if (_pause == null) Debug.LogError("pause menu is null");
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if(__hasTriggered == false)
			{
				_pause.PopUpPromptEnable(popUpPrompt);
				__hasTriggered = true;
			}
		}
	}
}
