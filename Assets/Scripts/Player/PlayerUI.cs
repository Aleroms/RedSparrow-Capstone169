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


}
