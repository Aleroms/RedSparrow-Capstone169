using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	
    
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
		SceneManager.LoadScene("Credits");
		//set game over panel on
		//should have a button that quits
		//should have button that takes to main menu
	}
}
