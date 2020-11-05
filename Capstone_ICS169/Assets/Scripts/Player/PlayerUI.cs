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
	public Text pickupPrompt;//weaponUI should not be here. This should only be playerUI
	public float pickupPromptTimer;//same with this

	
	

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
