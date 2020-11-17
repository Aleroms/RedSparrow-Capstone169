using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//attach script to canvas
//also, this script could be cut out and put into UIManager, i feel;
public class PauseMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _PauseMenuPanel;
	[SerializeField]
	private GameObject _OptionsMenuPanel;
	[SerializeField]
	private GameObject _healthBar;
	[SerializeField]
	private GameObject _staminaBar;

	private bool _isPaused = false;

	public Text mouseInput;
	public Text volumeInput;

	public Slider mouseSlider;
	public Slider volumeSlider;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (_isPaused == false)
				Pause();
			else
				Resume();
		}
		
	}
	private void Pause()
	{
		_isPaused = true;

		_PauseMenuPanel.SetActive(true);
		_healthBar.SetActive(false);
		_staminaBar.SetActive(false);

		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.None;
		//so that the player can select and press the buttons
	}
	public void Resume()
	{
		_isPaused = false;

		_PauseMenuPanel.SetActive(false);
		_OptionsMenuPanel.SetActive(false);
		_healthBar.SetActive(true);
		_staminaBar.SetActive(true);

		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
	}
	public void RestartLevel()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void Restore()
	{
		mouseInput.text = "3.2";
		volumeInput.text = "70";

		//StatsData.mouseSensitivity = 3.2f * 10 + 70;
		//StatsData.volumeLevel = 100;

		mouseSlider.value = 3.2f;
		volumeSlider.value = 70;

	}
	public void MouseSensitivity(float input)
	{
		mouseInput.text = input.ToString("F1");
		//StatsData.mouseSensitivity = input * 10 + 70;

		// N*10 + 70
	}
	public void VolumeLevel(float input)
	{
		volumeInput.text = input.ToString();
		//StatsData.volumeLevel = input;

		

	}
}
