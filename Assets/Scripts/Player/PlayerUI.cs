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

	[SerializeField]
	private bool _isFirstLevel = false;
	

	public void SetMaxHealth(int h)
	{
		if(_isFirstLevel == false)
			_healthSlider.maxValue = h;
	}
	public void SetMaxStamina(float s)
	{
		if (_isFirstLevel == false)
			_staminaSlider.maxValue = s;
	}
	
	public void HealthBar(int health)
	{
		if (_isFirstLevel == false)
			_healthSlider.value = health;
	}
	public void StaminaBar(float stamina)
	{
		if (_isFirstLevel == false)
			_staminaSlider.value = stamina;
	}


}
