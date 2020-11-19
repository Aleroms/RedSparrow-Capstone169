using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _OnPlayerDeath_Panel;
	[SerializeField]
	private GameObject _gameover_text;
	private bool temp = true;

	[SerializeField]
	private float textFlickerSpeed = 0.4f;

	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void Quit()
	{
		print("Closing Scene");
		Application.Quit();
	}
	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}
	public void GameOver()
	{
		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.None;
		_OnPlayerDeath_Panel.SetActive(true);
		StartCoroutine(GameOverCoroutine());
		//SceneManager.LoadScene("Credits");
		//set game over panel on
		//should have a button that quits
		//should have button that takes to main menu
	}
	IEnumerator GameOverCoroutine()
	{
		while(true)
		{
			print("e");
			_gameover_text.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			_gameover_text.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.5f);
		}
	}
	public void MainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
}
