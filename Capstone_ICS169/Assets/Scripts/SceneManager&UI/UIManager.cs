using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Button _playButton;
	[SerializeField]
	private Button _quitButton;
    // Start is called before the first frame update
    void Start()
    {
		
    }
	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void Quit()
	{
		print("Closing Scene");
		Application.Quit();
	}
}
