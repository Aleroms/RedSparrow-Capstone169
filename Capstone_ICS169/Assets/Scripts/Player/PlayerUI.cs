using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	[SerializeField]
	private Slider _healthSlider;
	[SerializeField]
	private Slider _staminaSlider;
	public Text pickupPrompt;
	public float pickupPromptTimer;
	/*
	[SerializeField]
	private Image _healthBarUI;
	[SerializeField]
	private Image _staminaBarUI;
	//[SerializeField]
	//private Image _ammoBarUI;
 
	*/

	public void SetMaxHealth(int h)
	{
		_healthSlider.maxValue = h;
	}
	public void SetMaxStamina(float s)
	{
		_staminaSlider.maxValue = s;
	}
	
	public void HealthBar(int health)
	{
		_healthSlider.value = health;
	}
	public void StaminaBar(float stamina)
	{
		_staminaSlider.value = stamina;
	}

	private void Update()
	{
		if (pickupPromptTimer > 0)
		{
			pickupPromptTimer -= Time.deltaTime;
			if (pickupPromptTimer <= 0)
			{
				pickupPromptTimer = 0;
				//pickupPrompt.enabled = false;
				pickupPrompt.gameObject.SetActive(false);
			}
				
		}
			

	}
}
